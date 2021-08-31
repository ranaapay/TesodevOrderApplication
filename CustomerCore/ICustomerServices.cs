using System.Collections.Generic;

namespace CustomerCore
{
    public interface ICustomerServices
    {
        List<Customer> GetCustomers();
        Customer CreateCustomer(Customer customer);
        Customer GetCustomer(string id);
        void DeleteCustomer(string id);
        Customer UpdateCustomer(Customer customer);
    }
}