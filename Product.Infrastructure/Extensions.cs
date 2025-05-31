using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain;

namespace Product.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));
            services.AddHostedService<DatabaseMigrationService>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ProductSeeder>();
            services.AddHostedService<DatabaseSeeder>();

            return services;
        }
    }
}
