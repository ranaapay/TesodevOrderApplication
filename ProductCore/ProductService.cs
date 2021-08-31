using System.Collections.Generic;
using MongoDB.Driver;

namespace ProductCore
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
        }
        
        public List<Product> Get() =>
            _products.Find(product => true).ToList();

        public Product Get(string id) =>
            _products.Find<Product>(product => product.Id == id).FirstOrDefault();

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }
        
        public void Update(string id, Product product) =>
            _products.ReplaceOne(p => p.Id == id, product);
        
        public void Remove(string id) => 
            _products.DeleteOne(product => product.Id == id);
    }
}