namespace Aggreation_Api.Models
{
    public class OrderForPostRequestModel
    {
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string ProductId { get; set; }
    }
}