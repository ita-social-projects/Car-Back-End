using System.Collections.Generic;
using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Car : IEntityWithImage
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Color Color { get; set; }

        public string? PlateNumber { get; set; }

        public string? ImageId { get; set; }

        public int OwnerId { get; set; }

        public ICollection<Journey> Journeys { get; set; } = new List<Journey>();

        public User? Owner { get; set; }

        public Model? Model { get; set; }
    }
}
