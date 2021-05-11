using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Models.Location
{
    public class CreateLocationModel
    {
        public string Name { get; set; }

        public AddressDto Address { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }
    }
}
