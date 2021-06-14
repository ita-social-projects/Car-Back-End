namespace Car.Domain.Dto.Address
{
    public class UpdateAddressToLocationDto
    {
        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
