using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Aggregates;

namespace Cart.Domain.Events.Product
{
    public record ProductRestored(Guid ItemId, int Quantity);
}
