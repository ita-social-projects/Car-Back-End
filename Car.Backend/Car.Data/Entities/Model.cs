using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Car.Data.Entities
{
    public class Model : IEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }

        [JsonIgnore]
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
