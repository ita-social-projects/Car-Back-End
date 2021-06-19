namespace Car.Data.Entities
{
    public class Schedule : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Journey? Journey { get; set; }
    }
}
