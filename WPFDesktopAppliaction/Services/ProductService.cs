using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPFDesktopAppliaction.Intefaces;
using WPFDesktopAppliaction.Models;

namespace WPFDesktopAppliaction.Services
{
    internal class ProductService : IProductService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("http://product:8080/product");
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();

            var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(contentStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return products;
        }
    }
}
