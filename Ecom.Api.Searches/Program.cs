using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IServiceCollection services = new ServiceCollection();
services.AddLogging();

builder.Services.AddTransient<ISearchService, SerachService>();
builder.Services.AddTransient<IOrderServices, OrderService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
});

builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
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
