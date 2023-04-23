using Ecom.Api.Products.DB;
using Ecom.Api.Products.Interfaces;
using Ecom.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductInterface, ProductsProvider>();
builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddDbContext<ProductsDbContext>(options =>
    {
        options.UseInMemoryDatabase("Products");
    
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
