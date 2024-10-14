using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models.DTO
{
    public class AddOrUpdateCustomerDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "First Name can be maximum of 50 characters")]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Last Name can be maximum of 50 characters")]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not in a valid format")]
        public required string Email { get; set; }

        [Required]
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

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
