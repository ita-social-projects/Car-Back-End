using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Car.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Model> Models { get; set; }

        [JsonIgnore]
        public Car Car { get; set; }
    }
}
