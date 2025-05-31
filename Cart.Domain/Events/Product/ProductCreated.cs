using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Events.Product
{
    public record ProductCreated
    { 
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    };
}
