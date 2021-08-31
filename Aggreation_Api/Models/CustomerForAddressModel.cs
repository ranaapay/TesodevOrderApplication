namespace Aggreation_Api.Models
{
    public class CustomerForAddressModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
    }
}