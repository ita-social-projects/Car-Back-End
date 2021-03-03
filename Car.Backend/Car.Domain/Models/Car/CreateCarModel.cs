using Car.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Models.Car
{
    public class CreateCarModel
    {
        public int OwnerId { get; set; }

        public int ModelId { get; set; }

        public Color Color { get; set; }

        public string PlateNumber { get; set; }

        public IFormFile Image { get; set; }
    }
}
