using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Domain;

namespace Product.Infrastructure
{
    internal class ProductRepository(ProductDbContext productDbContext) : IProductRepository
    {
        private readonly ProductDbContext _productDbContext = productDbContext;
        private readonly DbSet<Domain.Product> _products = productDbContext.Products;

        public async Task<IEnumerable<Domain.Product>> GetAll()
        {
            var products = await _products.ToListAsync();
            return products;
        }

        public async Task ReserveProduct(Guid productId, int quantity)
        {
            var product = await _products.FirstOrDefaultAsync(x => x.Id == productId) ?? throw new Exception("No product with given id");

            product.Quantity -= quantity;
            await _productDbContext.SaveChangesAsync();
        }

        public async Task RestoreProduct(Guid productId, int quantity)
        {
            var product = await _products.FirstOrDefaultAsync(x => x.Id == productId) ?? throw new Exception("No product with given id");

            product.Quantity += quantity;
            await _productDbContext.SaveChangesAsync();
        }
    }
}
