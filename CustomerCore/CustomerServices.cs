using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace CustomerCore
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IMongoCollection<Customer> _customers;
        
        public CustomerServices(ICustomerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Customer>(settings.CustomersCollectionName);
        }
        public List<Customer> GetCustomers()
        {
            return _customers.Find(customer => true).ToList();
        }

        public Customer CreateCustomer(Customer customer)
        {
            customer.CreatedAt= DateTime.Now;
            _customers.InsertOne(customer);
            return customer;
        }

        public Customer GetCustomer(string id)
        {
            return _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();
        }

        public void DeleteCustomer(string id)
        {
            _customers.DeleteOne(customer => customer.Id == id);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            GetCustomer(customer.Id);
            customer.UpdatedAt=DateTime.Now;
            _customers.ReplaceOne(c => c.Id == customer.Id, customer);
            return customer;
        }
        
    }
}