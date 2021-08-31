using MongoDB.Bson.Serialization.Attributes;

namespace OrderCore.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}