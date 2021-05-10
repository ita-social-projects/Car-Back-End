using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class LocationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LocationType Type { get; set; }

        public AddressDto Address { get; set; }

        public int UserId { get; set; }
    }
}
