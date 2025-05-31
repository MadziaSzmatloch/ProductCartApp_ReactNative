using Cart.Domain.Events.Cart;
using Cart.Domain.Interfaces;
using Hangfire;
using Marten;
using MediatR;

namespace Cart.Application.Commands.CreateCart
{
    public class CreateCartHandler(IDocumentStore documentStore, IBackgroundJobClient backgroundJobClient, IMediator mediator, IJobRepository jobRepository) : IRequestHandler<CreateCartRequest, CartCreated>
    {
        private readonly IDocumentStore _documentStore = documentStore;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IMediator _mediator = mediator;
        private readonly IJobRepository _jobRepository = jobRepository;

        public async Task<CartCreated> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = new CartCreated
            {
                UserId = request.userId,
            };
            await using var session = _documentStore.LightweightSession();
            session.Events.StartStream<Domain.Aggregates.Cart>(cart.Id, cart);
            await session.SaveChangesAsync();

            //TimeSpan delay = TimeSpan.FromMinutes(1);
            //var job = _backgroundJobClient.Schedule(() => DeleteCart(cart.Id), delay);
            var helper = new JobSchedulerHelper(_jobRepository, _backgroundJobClient, _mediator);
            helper.ScheduleJob(cart.Id);
            return cart;
        }

        
    }
}
