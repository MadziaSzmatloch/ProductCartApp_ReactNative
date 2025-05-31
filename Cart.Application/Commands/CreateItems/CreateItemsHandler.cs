using Cart.Domain.Events.Product;
using Marten;
using MediatR;

namespace Cart.Application.Commands.CreateItems
{
    public class CreateItemsHandler(IDocumentStore documentStore) : IRequestHandler<CreateItemsRequest, List<ProductCreated>>
    {
        private readonly IDocumentStore _documentStore = documentStore;
        async Task<List<ProductCreated>> IRequestHandler<CreateItemsRequest, List<ProductCreated>>.Handle(CreateItemsRequest request, CancellationToken cancellationToken)
        {
            var items = new List<ProductCreated>
            {
                new ProductCreated
                {
                    Id = Guid.NewGuid(),
                    Name = "Shirt",
                    Price = 100,
                    Quantity = 20
                },
                new ProductCreated
                {
                    Id = Guid.NewGuid(),
                    Name = "Skirt",
                    Price = 150,
                    Quantity = 40
                }
            };
            await using var session = _documentStore.LightweightSession();
            foreach (var item in items)
            {
                session.Events.StartStream<Domain.Aggregates.Item>(item.Id, item);
            }

            await session.SaveChangesAsync();
            return items;
        }
    }
}
