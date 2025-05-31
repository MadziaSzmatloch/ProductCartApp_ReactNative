using Cart.Domain.Aggregates;
using Cart.Domain.Events.Cart;
using Cart.Domain.Events.Product;
using Marten.Events.Aggregation;

namespace Cart.Domain.Projections
{
    public class CartProjection : SingleStreamProjection<Aggregates.Cart>
    {
        public void Apply(CartCreated created, Aggregates.Cart cart)
        {
            cart.Id = created.Id;
            cart.UserId = created.UserId;
        }

        public void Apply(ProductAdded productAdded, Aggregates.Cart cart)
        {
            var existingItem = cart.Items.FirstOrDefault(x => x.ItemId == productAdded.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += productAdded.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    CartId = productAdded.CartId,
                    ItemId = productAdded.ProductId,
                    Name = productAdded.Name,
                    Price = productAdded.Price,
                    Quantity = productAdded.Quantity
                });
            }

            cart.TotalPrice += productAdded.Price * productAdded.Quantity;
        }


        public void Apply(ProductRemoved productRemoved, Aggregates.Cart cart)
        {
            var cartItem = cart.Items.Find(x => x.ItemId == productRemoved.ItemId);
            if (cartItem.Quantity > productRemoved.Quantity)
            {
                cartItem.Quantity -= productRemoved.Quantity;
            }
            else if (cartItem.Quantity == productRemoved.Quantity)
            {
                cart.Items.Remove(cartItem);
            }

            cart.TotalPrice -= cartItem.Price * productRemoved.Quantity;
        }

        public void Apply(CartDeleted created, Aggregates.Cart cart)
        {
            cart.IsDeleted = true;
        }

        public void Apply(CartFinalized created, Aggregates.Cart cart)
        {
            cart.IsFinalized = true;
        }
    }
}
