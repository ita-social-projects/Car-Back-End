using Car.Data.Entities;
using Car.Data.Enums;

namespace Car.Domain.Dto
{
    public class CarDto
    {
        public int Id { get; set; }

        public ModelDto? Model { get; set; }

        public Color Color { get; set; }

        public string? PlateNumber { get; set; }

        public int OwnerId { get; set; }

        public string? ImageId { get; set; }
    }
}
