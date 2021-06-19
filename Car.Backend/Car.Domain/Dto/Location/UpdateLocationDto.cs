using Car.Domain.Dto.Address;

namespace Car.Domain.Dto.Location
{
    public class UpdateLocationDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public UpdateAddressToLocationDto? Address { get; set; }

        public int TypeId { get; set; }
    }
}
