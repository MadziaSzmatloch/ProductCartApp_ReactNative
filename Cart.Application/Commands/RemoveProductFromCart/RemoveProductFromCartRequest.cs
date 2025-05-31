using MediatR;

namespace Cart.Application.Commands.RemoveProductFromCart
{
    public record RemoveProductFromCartRequest(Guid CartId, Guid ProductId, int Quantity) : IRequest
    {
    }
}
