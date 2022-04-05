using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car.Domain.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string? Position { get; set; }

        public string? Location { get; set; }

        public DateTime HireDate { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? ImageId { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsPolicyAccepted { get; set; } = false;
    }
}
