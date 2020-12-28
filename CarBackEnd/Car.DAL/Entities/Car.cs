﻿namespace Car.DAL.Entities
{
    public class Car : IEntity, IEntityWithImage
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string PlateNumber { get; set; }

        public string ImageId { get; set; }

        public int UserId { get; set; }

        public User Owner { get; set; }
    }
}
