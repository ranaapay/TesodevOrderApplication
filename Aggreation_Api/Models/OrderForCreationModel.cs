namespace Aggreation_Api.Models
{
    public class OrderForCreationModel
    {
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public AddressModel Address { get; set; }
        public ProductModel Product { get; set; }
    }
}