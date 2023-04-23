using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;
using System.Text.Json;

namespace Ecom.Api.Searches.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<ProductService> logger;
        public ProductService(IHttpClientFactory httpClient, Logger<ProductService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<(bool isSuccess, IEnumerable<Product> product, string errorMessage)> GetProductAsync()
        {
            try
            {
                var client = httpClient.CreateClient("ProductService");
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var response = await client.GetAsync($"api/products");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var results = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, results, null);
                }
                return (false, null, response.ReasonPhrase.ToString());
            }
            catch(Exception ex)
            {
                this.logger?.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
