using Cart.Domain.Events.Cart;
using Cart.Domain.Events.Product;

namespace Cart.Domain.Aggregates
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public double TotalPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsFinalized { get; set; } = false;


        public void Apply(CartCreated evt)
        {
            UserId = evt.UserId;
        }

        public void Apply(ProductAdded evt)
        {
            var existingItem = Items.FirstOrDefault(x => x.ItemId == evt.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += evt.Quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    CartId = evt.CartId,
                    ItemId = evt.ProductId,
                    Name = evt.Name,
                    Price = evt.Price,
                    Quantity = evt.Quantity
                });
            }

            TotalPrice += (evt.Price * evt.Quantity);
        }


        public void Apply(ProductRemoved evt)
        {
            var cartItem = Items.Find(x => x.ItemId == evt.ItemId);
            if(cartItem.Quantity > evt.Quantity)
            {
                cartItem.Quantity -= evt.Quantity;
            }
            else if (cartItem.Quantity == evt.Quantity)
            {
                Items.Remove(cartItem);
            }

            TotalPrice -= (cartItem.Price * evt.Quantity);
        }

        public void Apply(CartDeleted evt)
        {
            IsDeleted = true;
        }

        public void Apply(CartFinalized evt)
        {
            IsFinalized = true;
        }
    }

}
