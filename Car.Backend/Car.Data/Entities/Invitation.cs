using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Invitation : IEntity
    {
        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        public int JourneyId { get; set; }

        public InvitationType Type { get; set; }

        public User? InvitedUser { get; set; }

        public Journey? Journey { get; set; }
    }
}
