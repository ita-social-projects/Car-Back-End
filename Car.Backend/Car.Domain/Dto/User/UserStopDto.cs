using Car.Data.Enums;

namespace Car.Domain.Dto.User
{
    public class UserStopDto
    {
        public int UserId { get; set; }

        public int StopId { get; set; }

        public StopType StopType { get; set; }
    }
}
