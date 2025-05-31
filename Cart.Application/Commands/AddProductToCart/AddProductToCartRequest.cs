using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands.AddProductToCart
{
    public record AddProductToCartRequest(Guid CartId, Guid ProductId, int Quantity) : IRequest;
}
