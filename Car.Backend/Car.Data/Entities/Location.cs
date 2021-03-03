namespace Car.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public int AddressId { get; set; }

        public int UserId { get; set; }

        public Address Address { get; set; }

        public LocationType Type { get; set; }

        public User User { get; set; }
    }
}
