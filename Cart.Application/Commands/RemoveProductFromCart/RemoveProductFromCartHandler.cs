using Cart.Domain.Aggregates;
using Cart.Domain.Events.Product;
using Cart.Domain.Interfaces;
using Hangfire;
using Marten;
using MediatR;

namespace Cart.Application.Commands.RemoveProductFromCart
{
    public class RemoveProductFromCartHandler(IDocumentSession documentSession, IJobRepository jobRepository, IBackgroundJobClient backgroundJobClient, IMediator mediator) : IRequestHandler<RemoveProductFromCartRequest>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly IJobRepository _jobRepository = jobRepository;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IMediator _mediator = mediator;

        public async Task Handle(RemoveProductFromCartRequest request, CancellationToken cancellationToken)
        {
            var helper = new ProductsHelper();
            var items = await helper.GetItemsAsync();
            var item = items.FirstOrDefault(i => i.Id == request.ProductId) ?? throw new InvalidOperationException("Produkt nie istnieje");
            
            var cart = await _documentSession.LoadAsync<Domain.Aggregates.Cart>(request.CartId, cancellationToken);
            if (cart == null)
                throw new InvalidOperationException("Koszyk nie istnieje");
            
            if (cart.IsDeleted)
                throw new InvalidOperationException("Koszyk został usunięty");
            if (!cart.Items.Any(i => i.ItemId == item.Id))
                throw new InvalidOperationException("Nie ma tego produktu w koszyku");

            if (cart.Items.Where(i => i.ItemId == item.Id).ToList().Count() < request.Quantity)
                throw new InvalidOperationException("Niewystarczjaca ilość produktu w koszyku");


            var cartStreamState = await _documentSession.Events.FetchStreamStateAsync(cart.Id, cancellationToken);
            _documentSession.Events.Append(request.CartId, cartStreamState!.Version + 1, new ProductRemoved(request.CartId, item.Id, request.Quantity));
            await helper.ReserveProductAsync(request.ProductId, request.Quantity);

            var jobHelper = new JobSchedulerHelper(_jobRepository, _backgroundJobClient, _mediator);
            await jobHelper.RescheduleJob(request.CartId);

            await _documentSession.SaveChangesAsync();

        }
    }
}
