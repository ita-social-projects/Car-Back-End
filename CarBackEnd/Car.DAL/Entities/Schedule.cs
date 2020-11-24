
namespace Car.DAL.Entities
{
    public class Schedule : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Journey Journey { get; set; }
    }
}
