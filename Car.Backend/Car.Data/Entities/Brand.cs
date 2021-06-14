using System.Collections.Generic;
using Newtonsoft.Json;

namespace Car.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Model> Models { get; set; } = new List<Model>();
    }
}
