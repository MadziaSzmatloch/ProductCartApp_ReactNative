using System.Text;
using System.Text.Json;
using Cart.Domain.Events.Cart;
using Cart.Domain.Interfaces;
using Hangfire;
using Marten;
using MediatR;

namespace Cart.Application.Commands.FinalizeCart
{
    public class FinalizeCartHandler(IMediator mediator, IDocumentSession documentSession, IJobRepository jobRepository, IBackgroundJobClient backgroundJobClient) : IRequestHandler<FinalizeCartRequest>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly IJobRepository _jobRepository = jobRepository;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;

        public async Task Handle(FinalizeCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _documentSession.LoadAsync<Domain.Aggregates.Cart>(request.CartId, cancellationToken);
            if (cart == null || cart.IsDeleted || cart.IsFinalized)
                throw new InvalidOperationException("Koszyk nie istnieje");

            var cartStreamState = await _documentSession.Events.FetchStreamStateAsync(cart.Id, cancellationToken);
            _documentSession.Events.Append(request.CartId, cartStreamState!.Version + 1, new CartFinalized(request.CartId));


            await _documentSession.SaveChangesAsync();
          
            var helper = new JobSchedulerHelper(_jobRepository, _backgroundJobClient, _mediator);
            await helper.DeleteJob(request.CartId);
        }
    }
}
