using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Events.Cart;
using MediatR;

namespace Cart.Application.Commands.CreateCart
{
    public record CreateCartRequest(Guid userId) : IRequest<CartCreated>;
}
