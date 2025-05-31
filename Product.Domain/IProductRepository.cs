using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Domain.Product>> GetAll();
        public Task ReserveProduct(Guid productId, int quantity);
        public Task RestoreProduct(Guid productId, int quantity);
    }
}
