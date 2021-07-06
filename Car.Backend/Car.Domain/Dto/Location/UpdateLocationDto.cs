using Car.Domain.Dto.Address;

namespace Car.Domain.Dto.Location
{
    public record UpdateLocationDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public UpdateAddressToLocationDto? Address { get; init; }

        public int TypeId { get; init; }
    }
}
