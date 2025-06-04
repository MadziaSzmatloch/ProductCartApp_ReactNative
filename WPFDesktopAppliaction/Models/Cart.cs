using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopAppliaction.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public double TotalPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsFinalized { get; set; } = false;

    }
}
