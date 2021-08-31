using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Aggreation_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aggreation_Api.Services
{
    public class CustomerGwService: ICustomerGwService
    {
        private readonly HttpClient _client;

        public CustomerGwService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            return await _client.GetFromJsonAsync<List<CustomerModel>>("/api/customer");
        }

        public async Task<CustomerModel> GetCustomer(string id)
        {
            return await _client.GetFromJsonAsync<CustomerModel>($"/api/customer/{id}");
        }
        
        public async Task<CustomerForAddressModel> GetCustomerForAddress(string id)
        {
            return await _client.GetFromJsonAsync<CustomerForAddressModel>($"/api/customer/address/{id}");
        }

        public async Task<CustomerModel> CreateCustomer(CustomerForCreationModel customer)
        {
            CustomerModel data;
            var postResponse = await _client.PostAsJsonAsync("/api/customer", customer);
            postResponse.EnsureSuccessStatusCode();
            var stream = await postResponse.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    data =  jsonSerializer.Deserialize<CustomerModel>(jsonTextReader);
                }
            }
            return data;
        }
        public async Task UpdateCustomer(string id, CustomerForCreationModel customer)
        {
            var postResponse = await _client.PutAsJsonAsync($"/api/customer/{id}", customer);
            postResponse.EnsureSuccessStatusCode();
        }
    }
}