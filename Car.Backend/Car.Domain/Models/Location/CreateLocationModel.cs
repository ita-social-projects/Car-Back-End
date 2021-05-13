using Car.Domain.Models.Journey;

namespace Car.Domain.Models.Location
{
    public class CreateLocationModel
    {
        public string Name { get; set; }

        public CreateAddressModel Address { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }
    }
}
