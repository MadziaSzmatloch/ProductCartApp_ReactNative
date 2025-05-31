using Cart.Domain.Events.Product;

namespace Cart.Domain.Aggregates
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Item() { }

        public Item(Guid id, string name, int quantity, double price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public void Apply(ProductCreated evt)
        {
            Id = evt.Id;
            Name = evt.Name;
            Price = evt.Price;
            Quantity = evt.Quantity;
        }

        public void Apply(ProductReserved evt)
        {
            Quantity -= evt.Quantity;
        }

        public void Apply(ProductRestored evt)
        {
            Quantity += evt.Quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
