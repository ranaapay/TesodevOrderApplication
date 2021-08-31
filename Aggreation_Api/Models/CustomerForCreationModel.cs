namespace Aggreation_Api.Models
{
    public class CustomerForCreationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
    }
}