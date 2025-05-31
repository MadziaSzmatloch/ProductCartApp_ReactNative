using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Events.Product;
using MediatR;

namespace Cart.Application.Commands.CreateItems
{
    public record CreateItemsRequest : IRequest<List<ProductCreated>>;
}
