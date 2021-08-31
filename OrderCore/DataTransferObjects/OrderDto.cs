using System.Reflection;

namespace OrderCore.DataTransferObjects
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string FullAdress { get; set; } 
        public string ProductProperties { get; set; }
    }
}