using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cart.Domain.Aggregates;
using Marten;

namespace Cart.Infrastructure.Seeders
{
    public class UsersSeeder
    {
        public static async Task SeedUsersAsync(IDocumentStore store)
        {
            using var session = store.LightweightSession();

            if (await session.Query<User>().AnyAsync()) return;

            var items = new List<User>
            {
                new() { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Charlie", Email = "charlie@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Diana", Email = "diana@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Eve", Email = "eve@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Frank", Email = "frank@example.com" }
            };

            session.StoreObjects(items);
            await session.SaveChangesAsync();
        }
    }
}
