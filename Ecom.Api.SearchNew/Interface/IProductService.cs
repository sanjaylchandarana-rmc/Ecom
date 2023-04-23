using Ecom.Api.Searches.Models;

namespace Ecom.Api.Searches.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSuccess, IEnumerable<Product> product, string errorMessage)> GetProductAsync();
    }
}
