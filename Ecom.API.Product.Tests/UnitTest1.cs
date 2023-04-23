using Xunit;
using System;
using Ecom.Api.Products.Providers;
using Ecom.Api.Products.DB;
using Microsoft.EntityFrameworkCore;
using Ecom.Api.Products.Profiles;
using AutoMapper;
using System.Linq;

namespace Ecom.Api.Product.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async void GetProductsReturnsAllProducts()
        {

            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);


            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var productProvider = new ProductsProvider(dbContext, null, mapper);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var product = await productProvider.GetProdutsAsync();
            Assert.True(product.isSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
             
        }
        private void CreateProduct(ProductsDbContext dbContext)
        {
            for(int i=1;i <=10; i++)
            {

                dbContext.Products.Add(new Ecom.Api.Products.DB.Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)

                });
            }
            dbContext.SaveChanges();
        }
        [Fact]
        public async void GetProductsReturnsProductUsingVaildId()
        {

            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingVaildId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);


            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var productProvider = new ProductsProvider(dbContext, null, mapper);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var product = await productProvider.GetProductAsync(1);
            Assert.True(product.isSuccess);
            Assert.NotNull(product.Products);
            Assert.True(product.Products.Id == 1);
            Assert.Null(product.ErrorMessage);

        }
        [Fact]
        public async void GetProductsReturnsProductUsingInVaildId()
        {

            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInVaildId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);


            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var productProvider = new ProductsProvider(dbContext, null, mapper);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var product = await productProvider.GetProductAsync(-1);
            Assert.False(product.isSuccess);
            Assert.Null(product.Products);        
            Assert.NotNull(product.ErrorMessage);

        }
    }
}