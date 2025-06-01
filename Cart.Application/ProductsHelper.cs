using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cart.Domain.Aggregates;

namespace Cart.Application
{
    public class ProductsHelper
    {

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://product:8080/product");

            response.EnsureSuccessStatusCode();

            var contentStream = await response.Content.ReadAsStreamAsync();

            var items = await JsonSerializer.DeserializeAsync<IEnumerable<Item>>(contentStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return items;
        }

        public async Task ReserveProductAsync(Guid productId, int quantity)
        {
            using var httpClient = new HttpClient();

            var request = new
            {
                ProductId = productId,
                Quantity = quantity
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync("http://product:8080/product/reserve", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RestoreProductAsync(Guid productId, int quantity)
        {
            using var httpClient = new HttpClient();

            var request = new
            {
                ProductId = productId,
                Quantity = quantity
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync("http://product:8080/product/restore", content);
            response.EnsureSuccessStatusCode();
        }


    }
}
