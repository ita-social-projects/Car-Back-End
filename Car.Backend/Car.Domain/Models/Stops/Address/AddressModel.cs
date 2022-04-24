namespace Car.Domain.Models.Stops
{
    public class AddressModel
    {
        public string Name { get; init; } = string.Empty;

        public double Latitude { get; init; }

        public double Longitude { get; init; }
    }
}