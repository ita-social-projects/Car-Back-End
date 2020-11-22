
namespace Car.DAL.Entities
{
    public class UserPreferences : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool DoAllowSmoking { get; set; }
        public bool DoAllowEating { get; set; }
        public string Comments { get; set; }

        public User User { get; set; }
    }
}
