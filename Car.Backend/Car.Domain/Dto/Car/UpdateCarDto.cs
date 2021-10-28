using Car.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Dto
{
    public class UpdateCarDto
    {
        public int Id { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public Color Color { get; set; }

        public string? PlateNumber { get; set; }

        public IFormFile? Image { get; set; }
    }
}
