using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class UserStop : IEntity
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int StopId { get; set; }

        public Stop? Stop { get; set; }

        public StopType StopType { get; set; }
    }
}