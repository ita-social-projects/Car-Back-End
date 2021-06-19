using Car.Domain.Dto.Address;

namespace Car.Domain.Dto.Location
{
    public class LocationDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public AddressDto? Address { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }
    }
}
