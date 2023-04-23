using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;

namespace Ecom.Api.Searches.Services
{
    public class SerachService : ISearchService
    {

        private readonly IOrderServices orderService;
        private readonly IProductService productService;

        public SerachService(IOrderServices orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        public async Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrderAsync(customerId); 
            var productResult = await productService.GetProductAsync();
            if (orderResult.isSuccess)
            {
                foreach(var order in orderResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.product.FirstOrDefault(p => p.Id == item.ProductId).Name.ToString();

                    }
                }



                var result = new
                {
                    Order = orderResult.Orders
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
