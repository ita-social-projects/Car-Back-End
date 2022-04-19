using Car.Domain.Dto.Address;

namespace Car.Domain.Dto.Location
{
    public class CreateLocationDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public AddressDto? Address { get; init; }

        public int TypeId { get; init; }
    }
}
