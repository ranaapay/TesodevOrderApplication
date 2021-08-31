using System;
using System.Collections.Generic;
using MongoDB.Driver;
using OrderCore.Models;

namespace OrderCore
{
    public class OrderServices : IOrderServices
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderServices(IOrderDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public List<Order> GetOrders()
        {
            return _orders.Find(order => true).ToList();
        }

        public Order GetOrder(string id)
        {
            return _orders.Find<Order>(order => order.Id == id).FirstOrDefault();
        }

        public Order AddOrder(Order order)
        {
            order.CreatedAt=DateTime.Now;
            _orders.InsertOne(order);
            return order;
        }

        public void DeleteOrder(string id)
        {
            _orders.DeleteOne(order => order.Id == id);
        }

        public Order UpdateOrder(Order order)
        {
            //databasede var mı diye kontrol için, yoksa exception atıcak
            GetOrder(order.Id);
            _orders.ReplaceOne(o => o.Id == order.Id, order);
            return order;
        }
    }
}