using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands.FinalizeCart
{
    public record FinalizeCartRequest(Guid CartId) : IRequest;
}
