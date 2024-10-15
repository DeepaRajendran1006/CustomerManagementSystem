using System.ComponentModel.DataAnnotations;

namespace Customer.UI.Models
{
    public class AddOrUpdateCustomerViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be more than 50 characters.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [MaxLength(10, ErrorMessage = "Phone number should be 10 digits")]
        [MinLength(10, ErrorMessage = "Phone number should be 10 digits")]
        [Phone(ErrorMessage = "Phone number is not in a valid format")]
        public required string PhoneNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Address can be maximum of 100 characters")]
        public string? Address { get; set; }

        [MaxLength(50, ErrorMessage = "City can be maximum of 50 characters")]
        public string? City { get; set; }

        [MaxLength(6, ErrorMessage = "Postcode can be maximum of 6 characters")]
        [MinLength(6, ErrorMessage = "Postcode can be minimum of 6 characters")]
        public string? Postcode { get; set; }

        [MaxLength(50, ErrorMessage = "Country can be maximum of 50 characters")]
        public string? Country { get; set; }

        public bool IsActive { get; set; }
    }
}
