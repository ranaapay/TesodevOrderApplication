using System.Collections.Generic;
using OrderCore.Models;

namespace OrderCore
{
    public interface IOrderServices
    {
        List<Order> GetOrders();
        Order GetOrder(string id);
        Order AddOrder(Order order);
        void DeleteOrder(string id);
        Order UpdateOrder(Order order);
    }
}