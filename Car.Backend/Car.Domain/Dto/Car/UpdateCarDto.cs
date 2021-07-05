using Car.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Dto
{
    public class UpdateCarDto
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Color Color { get; set; }

        public string PlateNumber { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}
