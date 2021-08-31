using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aggreation_Api.Models;

namespace Aggreation_Api.Services
{
    public interface IProductGwService
    {
        Task<ProductModel> GetProduct(string id);
        Task<IEnumerable<ProductModel>> GetProducts();

    }
}
