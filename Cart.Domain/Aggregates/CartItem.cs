using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Aggregates
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
