using System.Collections.Generic;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.User;

namespace Car.Domain.Dto.Stop
{
    public class StopDto
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public bool IsCancelled { get; set; }

        public AddressDto? Address { get; set; }

        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();

        public ICollection<UserStopDto> UserStops { get; set; } = new List<UserStopDto>();
    }
}
