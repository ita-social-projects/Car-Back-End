using Car.Data.Entities;
using Car.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Dto
{
    public class CreateCarDto
    {
        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public Color Color { get; set; }

        public string? PlateNumber { get; set; }

        public IFormFile? Image { get; set; }
    }
}
