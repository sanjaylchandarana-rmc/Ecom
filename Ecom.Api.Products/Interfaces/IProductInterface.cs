using Ecom.Api.Products.Model;

namespace Ecom.Api.Products.Interfaces
{
    public interface IProductInterface
    {
        Task<(bool isSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProdutsAsync();

        Task<(bool isSuccess, Product Products, string ErrorMessage)> GetProductAsync(int id);
    }
}
