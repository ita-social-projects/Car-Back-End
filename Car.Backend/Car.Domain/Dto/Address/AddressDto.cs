namespace Car.Domain.Dto.Address
{
    public record AddressDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public double Latitude { get; init; }

        public double Longitude { get; init; }
    }
}
