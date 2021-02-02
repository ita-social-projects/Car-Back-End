namespace Car.Data.Entities
{
    public class Car : IEntityWithImage
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public string Color { get; set; }

        public string PlateNumber { get; set; }

        public string ImageId { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public Model Model { get; set; }
    }
}
