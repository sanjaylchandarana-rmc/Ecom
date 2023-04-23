using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;
using System.Text.Json;

namespace Ecom.Api.Searches.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClient;
         
        public ProductService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
             
        }
        public async Task<(bool isSuccess, IEnumerable<Product> product, string errorMessage)> GetProductAsync()
        {
            try
            {
                var client = httpClient.CreateClient("ProductsService");
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
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
