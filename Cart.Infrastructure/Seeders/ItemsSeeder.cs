using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cart.Domain.Aggregates;
using Cart.Domain.Events;
using Marten;

namespace Cart.Infrastructure.Seeders
{
    public class ItemsSeeder
    {
            public static async Task SeedItemsAsync(IDocumentStore store)
            {
                //using var session = store.LightweightSession();

                //// Sprawdź, czy istnieją strumienie dla typu Item
                //var existingStreams = await session.Events.QueryRawEventDataOnly<Domain.Aggregates.Item>()
                //    .AnyAsync();
                //if (existingStreams) return;

                //var items = new List<(Guid Id, string Name, double Price, int Quantity)>
                //{
                //    (Guid.NewGuid(), "Shirt", 100, 20),
                //    (Guid.NewGuid(), "Skirt", 150, 40),
                //    (Guid.NewGuid(), "Shorts", 150, 30),
                //    (Guid.NewGuid(), "Trousers", 150, 60),
                //    (Guid.NewGuid(), "Dress", 150, 80),
                //    (Guid.NewGuid(), "Shoes", 150, 55),
                //};

                //foreach (var (id, name, price, quantity) in items)
                //{
                //    // Rozpocznij strumień zdarzeń dla Item
                //    session.Events.StartStream<Domain.Aggregates.Item>(
                //        id,
                //        new ProductCreated(id, name, price, quantity)
                //    );
                //}

                //await session.SaveChangesAsync();
            }
        }
    }
