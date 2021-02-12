using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();
    }
}
