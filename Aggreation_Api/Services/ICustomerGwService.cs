using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aggreation_Api.Models;

namespace Aggreation_Api.Services
{
    public interface ICustomerGwService
    {
        Task<IEnumerable<CustomerModel>> GetCustomers();
        Task<CustomerModel> GetCustomer(string id);
        Task<CustomerModel> CreateCustomer(CustomerForCreationModel customer);
        Task UpdateCustomer(string id, CustomerForCreationModel customer);
        Task<CustomerForAddressModel> GetCustomerForAddress(string id);



    }
}
