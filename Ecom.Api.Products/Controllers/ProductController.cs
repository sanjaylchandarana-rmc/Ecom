using Ecom.Api.Products.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _productProvider;
        public ProductController(IProductInterface productProvier)
        {
            this._productProvider = productProvier;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await _productProvider.GetProdutsAsync();
            if (result.isSuccess)
            {
                return Ok(result.Products);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productProvider.GetProductAsync(id);
            if(result.isSuccess)
            {
                return Ok(result.Products);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
