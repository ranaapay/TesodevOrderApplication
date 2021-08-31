using DnsClient.Protocol;

namespace CustomerCore.DataTransferObjects
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FullAdress { get; set; }
        
    }
}