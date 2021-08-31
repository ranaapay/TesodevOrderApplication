using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerCore
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}