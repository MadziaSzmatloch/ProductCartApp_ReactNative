using Cart.Domain.Events.Cart;
using Cart.Domain.Events.Product;
using Marten;
using MediatR;

namespace Cart.Application.Commands.DeleteCart
{
    public class DeleteCartHandler(IDocumentSession documentSession) : IRequestHandler<DeleteCartRequest>
    {
        private readonly IDocumentSession _documentSession = documentSession;

        public async Task Handle(DeleteCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _documentSession.LoadAsync<Domain.Aggregates.Cart>(request.CartId, cancellationToken);
            if (cart == null)
                throw new InvalidOperationException("Koszyk nie istnieje");

            if (cart.IsDeleted)
                throw new InvalidOperationException("Koszyk został usunięty");

            var cartStreamState = await _documentSession.Events.FetchStreamStateAsync(cart.Id, cancellationToken);
            _documentSession.Events.Append(request.CartId, cartStreamState!.Version + 1, new CartDeleted(request.CartId));



            var helper = new ProductsHelper();
            var items = cart.Items;
            foreach (var item in items)
            {
                await helper.RestoreProductAsync(item.Id, item.Quantity);
            }

            await _documentSession.SaveChangesAsync();
        }
    }
}
