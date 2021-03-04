using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class LocationType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
