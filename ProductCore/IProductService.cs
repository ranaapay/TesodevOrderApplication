using System.Collections.Generic;

namespace ProductCore
{
    public interface IProductService
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product product);
        void Update(string id, Product product);
        void Remove(string id);
    }
}