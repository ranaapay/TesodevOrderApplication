using System.ComponentModel.DataAnnotations;

namespace CustomerCore.DataTransferObjects
{
    public abstract class CustomerForManipulationDto
    {
        [Required(ErrorMessage = "Customer name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "All address fields are required fields.")]
        public AddressDto Address { get; set; }
    }
}