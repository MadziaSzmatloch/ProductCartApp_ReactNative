using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Product.Infrastructure
{
    public class ProductSeeder(ProductDbContext productDbContext)
    {
        private readonly ProductDbContext _productDbContext = productDbContext;

        public async Task Seed()
        {
            if (await _productDbContext.Database.CanConnectAsync())
            {
                if (!_productDbContext.Products.Any())
                {
                    var products = new List<Domain.Product>()
                    {
                        new Domain.Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Shirt",
                                Price = 100,
                                Quantity = 20
                            },
                            new Domain.Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Skirt",
                                Price = 150,
                                Quantity = 40
                            },
                            new Domain.Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Dress",
                                Price = 220,
                                Quantity = 35
                            }
                    };
                    await _productDbContext.Products.AddRangeAsync(products);
                    await _productDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
