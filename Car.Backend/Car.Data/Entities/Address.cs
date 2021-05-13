using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Location Location { get; set; }

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();
    }
}
