namespace Car.DAL.Entities
{
    public class UserPreferences : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool DoAllowSmoking { get; set; }

        public bool DoAllowEating { get; set; }

        public string Comments { get; set; }

        public User Owner { get; set; }
    }
}
