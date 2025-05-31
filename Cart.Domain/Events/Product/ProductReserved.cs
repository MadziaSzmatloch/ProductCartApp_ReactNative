using Cart.Domain.Aggregates;

namespace Cart.Domain.Events.Product
{
    public record ProductReserved(Guid ProductId, int Quantity);
}
