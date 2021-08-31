using System.Collections.Generic;
using System.Threading.Tasks;
using Aggreation_Api.Models;

namespace Aggreation_Api.Services
{
    public interface IOrderGwService
    {
        Task<IEnumerable<OrderModel>> GetOrders();
        Task<OrderModel> GetOrder(string id);
        Task<OrderModel> CreateOrder(OrderForCreationModel order);
        Task UpdateOrder(string id, OrderForCreationModel order);
        

    }
}