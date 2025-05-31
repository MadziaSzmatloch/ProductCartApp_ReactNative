using Cart.Domain.Aggregates;

namespace Cart.Domain.Events.Product
{
    public record ProductAdded(Guid CartId, Guid ProductId, string Name, double Price, int Quantity);
}
