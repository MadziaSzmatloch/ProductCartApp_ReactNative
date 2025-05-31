using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Domain;

namespace Product.Application.GetProducts
{
    public record GetProductsRequest : IRequest<IEnumerable<Domain.Product>>;
}
