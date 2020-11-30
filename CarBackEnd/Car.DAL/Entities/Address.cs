namespace Car.DAL.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Stop Stop { get; set; }
    }
}
