using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;

namespace Ecom.Api.Searches.Services
{
    public class SerachService : ISearchService
    {

        private readonly IOrderServices orderService;
        private readonly IProductService productService;
        private readonly ICustomersService customersService;

        public SerachService(IOrderServices orderService, IProductService productService, ICustomersService customersService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customersService = customersService;
        }

        public async Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var customersResult = await customersService.GetCustomerAsync(customerId);
            var orderResult = await orderService.GetOrderAsync(customerId); 
            var productResult = await productService.GetProductAsync();
            if (orderResult.isSuccess)
            {
                foreach(var order in orderResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.isSuccess ?
                            productResult.product.FirstOrDefault(p => p.Id == item.ProductId).Name.ToString() :
                            "Product Information is not available";

                    }
                }
                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                               customersResult.Customer :
                               new { Name = "Customer information is not available" },

                    Orders = orderResult.Orders
                };                
                return (true, result);
            }
            else
            {
                return (false, null);
            }
        }
    }
}
