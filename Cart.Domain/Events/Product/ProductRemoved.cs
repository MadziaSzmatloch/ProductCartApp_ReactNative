using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Aggregates;

namespace Cart.Domain.Events.Product
{
    public record ProductRemoved(Guid CartId, Guid ItemId, int Quantity);
}
