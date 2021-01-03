﻿using System.Text.Json.Serialization;

namespace Car.DAL.Entities
{
    public class Model : IEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }

        [JsonIgnore]
        public Car Car { get; set; }
    }
}
