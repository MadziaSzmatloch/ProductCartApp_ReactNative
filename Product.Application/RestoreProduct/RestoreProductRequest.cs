using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.RestoreProduct
{
    public record RestoreProductRequest(Guid ProductId, int Quantity) : IRequest;
}
