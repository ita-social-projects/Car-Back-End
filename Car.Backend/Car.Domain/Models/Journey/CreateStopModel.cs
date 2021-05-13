using Car.Data.Enums;

namespace Car.Domain.Models.Journey
{
    public class CreateStopModel
    {
        public StopType Type { get; set; }

        public CreateAddressModel Address { get; set; }

        public int UserId { get; set; }
    }
}