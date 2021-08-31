using OrderCore.Models;

namespace OrderCore.DataTransferObjects
{
    public class OrderForUpdateDto
    {
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }
    }
}