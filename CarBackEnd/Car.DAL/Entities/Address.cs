
namespace Car.DAL.Entities
{
    class Address : IEntityBase
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Stop Stop { get; set; }
    }
}
