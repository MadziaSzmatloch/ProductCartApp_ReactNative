using System.Text;
using System.Text.Json;
using Cart.Domain.Aggregates;
using Cart.Domain.Events.Product;
using Cart.Domain.Interfaces;
using Hangfire;
using Marten;
using MediatR;

namespace Cart.Application.Commands.AddProductToCart
{
    public class AddProductToCartHandler(IDocumentSession documentSession, IQuerySession querySession, IJobRepository jobRepository, IBackgroundJobClient backgroundJobClient, IMediator mediator) : IRequestHandler<AddProductToCartRequest>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly IQuerySession _querySession = querySession;
        private readonly IJobRepository _jobRepository = jobRepository;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IMediator _mediator = mediator;

        public async Task Handle(AddProductToCartRequest request, CancellationToken cancellationToken)
        {
            var helper = new ProductsHelper();
            var items = await helper.GetItemsAsync();
            var item = items.FirstOrDefault(i => i.Id == request.ProductId) ?? throw new InvalidOperationException("Produkt nie istnieje");

            if (request.Quantity > item.Quantity)
            {
                throw new InvalidOperationException("Niewystarczjaca ilość produktu");
            }
            var cart = await _documentSession.LoadAsync<Domain.Aggregates.Cart>(request.CartId) ?? throw new InvalidOperationException("Koszyk nie istnieje");

            if (cart.IsDeleted)
                throw new InvalidOperationException("Koszyk został usunięty");

            var cartStreamState = await _documentSession.Events.FetchStreamStateAsync(cart.Id);

            _documentSession.Events.Append(request.CartId, cartStreamState!.Version + 1, new ProductAdded(request.CartId, item.Id, item.Name, item.Price, request.Quantity));
            await helper.ReserveProductAsync(request.ProductId, request.Quantity);


            var jobHelper = new JobSchedulerHelper(_jobRepository, _backgroundJobClient, _mediator);
            await jobHelper.RescheduleJob(request.CartId);

            await _documentSession.SaveChangesAsync();
        }
    }
}
