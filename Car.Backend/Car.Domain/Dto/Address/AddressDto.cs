namespace Car.Domain.Dto.Address
{
    public class AddressDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
