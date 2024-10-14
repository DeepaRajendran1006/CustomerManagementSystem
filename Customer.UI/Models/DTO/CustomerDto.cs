﻿namespace Customer.UI.Models.DTO
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Postcode { get; set; }

        public string? Country { get; set; }

        public bool IsActive { get; set; }
    }
}
