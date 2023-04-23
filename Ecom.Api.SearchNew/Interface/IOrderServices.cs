using Ecom.Api.Searches.Models;

namespace Ecom.Api.Searches.Interfaces
{
    public interface IOrderServices
    {
        Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrderAsync(int customerId);

    }
}
