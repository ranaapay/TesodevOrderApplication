using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Aggreation_Api.Models;

namespace Aggreation_Api.Services
{
    public class ProductGwService : IProductGwService
    {
        private readonly HttpClient _client;

        public ProductGwService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ProductModel> GetProduct(string id)
        {
            return await _client.GetFromJsonAsync<ProductModel>($"/api/product/{id}");
        }
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            return await _client.GetFromJsonAsync<List<ProductModel>>("/api/product");
        }
    }
}