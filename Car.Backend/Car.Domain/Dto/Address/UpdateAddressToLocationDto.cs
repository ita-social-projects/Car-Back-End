namespace Car.Domain.Dto.Address
{
    public record UpdateAddressToLocationDto
    {
        public string Name { get; init; } = string.Empty;

        public double Latitude { get; init; }

        public double Longitude { get; init; }
    }
}
