using MongoDB.Bson.Serialization.Attributes;

namespace ProductCore
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}