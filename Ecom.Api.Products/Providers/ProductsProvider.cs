using AutoMapper;
using Ecom.Api.Products.DB;
using Ecom.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Api.Products.Providers
{
    public class ProductsProvider : IProductInterface
    {

        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        public void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product { Id = 1, Name = "KeyBoard", Price = 10, Inventory = 100 });
                dbContext.Products.Add(new DB.Product { Id = 2, Name = "Mouse", Price = 20, Inventory = 200 });
                dbContext.Products.Add(new DB.Product { Id = 3, Name = "CPU", Price = 30, Inventory = 10 });
                dbContext.Products.Add(new DB.Product { Id = 4, Name = "Monitor", Price = 40, Inventory = 500 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Model.Product> Products, string ErrorMessage)> GetProdutsAsync()
        {
            
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result =  mapper.Map<IEnumerable<DB.Product>, IEnumerable<Model.Product>>(products);

                    #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                    return (true, result, null);
                    #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

                }
                #pragma warning    disable CS8619 // Nullability of reference types in value doesn't match target type.
                return (false, null, "Not Found");
                #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                
                #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return (false, null, ex.Message.ToString());
                #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
        }

        public async Task<(bool isSuccess, Model.Product Products, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var pd = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if(pd != null)
                {
                    var result = mapper.Map<DB.Product, Model.Product>(pd);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
