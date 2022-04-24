using Car.Data.Enums;

namespace Car.Domain.Models.Stops
{
    public class StopModel
    {
        public StopType StopType { get; set; }

        public AddressModel? Address { get; set; }
    }
}