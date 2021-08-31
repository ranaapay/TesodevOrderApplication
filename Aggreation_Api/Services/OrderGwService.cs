using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Aggreation_Api.Models;
using Newtonsoft.Json;

namespace Aggreation_Api.Services
{
    public class OrderGwService : IOrderGwService
    {
        private readonly HttpClient _client;

        public OrderGwService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            return await _client.GetFromJsonAsync<List<OrderModel>>("/api/order");
        }
        
        public async Task<OrderModel> GetOrder(string id)
        {
            return await _client.GetFromJsonAsync<OrderModel>($"/api/order/{id}");
        }

        public async Task<OrderModel> CreateOrder(OrderForCreationModel order)
        {
            OrderModel data;
            var postResponse = await _client.PostAsJsonAsync("/api/order", order);
            postResponse.EnsureSuccessStatusCode();
            var stream = await postResponse.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    data =  jsonSerializer.Deserialize<OrderModel>(jsonTextReader);
                }
            }
            return data;
        }

        public async Task UpdateOrder(string id, OrderForCreationModel order)
        {
            var postResponse = await _client.PutAsJsonAsync($"/api/order/{id}", order);
            postResponse.EnsureSuccessStatusCode();
        }
    }
}