using MediatR;

namespace Cart.Application.Queries
{
    public record GetCartByIdRequest(Guid Id) : IRequest<Domain.Aggregates.Cart>;
}
