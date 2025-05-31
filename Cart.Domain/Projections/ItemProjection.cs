using Cart.Domain.Aggregates;
using Cart.Domain.Events.Product;
using Marten.Events.Aggregation;

namespace Cart.Domain.Projections
{
    public class ItemProjection : SingleStreamProjection<Item>
    {
        public ItemProjection() { }

        public void Apply(ProductCreated productAdded, Item item)
        {
            item.Id = productAdded.Id;
            item.Name = productAdded.Name;
            item.Price = productAdded.Price;
            item.Quantity = productAdded.Quantity;
        }

        public void Apply(ProductReserved productAdded, Item item)
        {
            item.Quantity -= productAdded.Quantity;
        }

        public void Apply(ProductRestored productAdded, Item item)
        {
            item.Quantity += productAdded.Quantity;
        }
    }
}
