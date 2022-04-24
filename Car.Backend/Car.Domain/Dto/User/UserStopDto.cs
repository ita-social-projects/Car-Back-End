using Car.Data.Enums;
using Car.Domain.Dto.Stop;

namespace Car.Domain.Dto.User
{
    public class UserStopDto
    {
        public int UserId { get; set; }

        public int StopId { get; set; }

        public StopType? StopType { get; set; }
    }
}
