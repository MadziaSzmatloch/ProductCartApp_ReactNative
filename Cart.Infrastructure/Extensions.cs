using Cart.Domain.Events;
using Cart.Domain.Interfaces;
using Cart.Domain.Projections;
using Cart.Infrastructure.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weasel.Core;

namespace Cart.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            services.AddMarten(options =>
            {
                options.Connection(connectionString);

                options.UseSystemTextJsonForSerialization();
                options.Projections.Add<CartProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);
                options.Projections.Add<ItemProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);

                options.AutoCreateSchemaObjects = AutoCreate.All;
            });

            services.AddScoped<IJobRepository>(provider =>
            {
                return new JobRepository(connectionString);
            });

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));
            services.AddHangfireServer();

            return services;
        }
    }
}
