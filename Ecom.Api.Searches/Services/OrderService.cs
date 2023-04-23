﻿using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;
using System.Text.Json;

namespace Ecom.Api.Searches.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;
        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderService");
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var response = await client.GetAsync($"api/orders/{customerId}");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var results = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, results, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
