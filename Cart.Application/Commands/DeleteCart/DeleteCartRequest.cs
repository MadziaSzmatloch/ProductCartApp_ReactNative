using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands.DeleteCart
{
    public record DeleteCartRequest(Guid CartId) : IRequest;
}
