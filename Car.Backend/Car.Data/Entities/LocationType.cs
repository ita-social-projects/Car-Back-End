using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class LocationType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
