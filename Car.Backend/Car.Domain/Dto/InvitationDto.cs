using Car.Data.Enums;
using Car.Domain.Dto.Address;

namespace Car.Domain.Dto
{
    public class InvitationDto
    {
        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        public int JourneyId { get; set; }

        public InvitationType Type { get; set; }
    }
}
