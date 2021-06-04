using Car.Domain.Models.Address;

namespace Car.Domain.Models.Location
{
    public class UpdateLocationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UpdateAddressToLocationModel Address { get; set; }

        public int TypeId { get; set; }
    }
}
