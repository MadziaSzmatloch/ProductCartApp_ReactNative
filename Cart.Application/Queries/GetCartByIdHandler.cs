using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Aggregates;
using Marten;
using Marten.Linq.Parsing.Operators;
using MediatR;
using static System.Collections.Specialized.BitVector32;

namespace Cart.Application.Queries
{
    public class GetCartByIdHandler(IQuerySession querySession) : IRequestHandler<GetCartByIdRequest, Domain.Aggregates.Cart>
    {
        private readonly IQuerySession _querySession = querySession;

        public async Task<Domain.Aggregates.Cart> Handle(GetCartByIdRequest request, CancellationToken cancellationToken)
        {
            var cart = await _querySession.LoadAsync<Domain.Aggregates.Cart>(request.Id);

            if (cart is null)
                throw new InvalidOperationException("There is no cart with given id!");
            if (cart.IsDeleted)
                throw new InvalidOperationException("Koszyk został usunięty");

            return cart;
        }
    }
}
