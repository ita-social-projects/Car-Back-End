using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Model> Models { get; set; } = new List<Model>();
    }
}
