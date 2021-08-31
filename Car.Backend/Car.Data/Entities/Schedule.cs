using System.Collections.Generic;
using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Schedule : IEntity
    {
        public int Id { get; set; }

        public WeekDay Days { get; set; }

        public Journey? Journey { get; set; }

        public ICollection<Journey> ChildJourneys { get; set; } = new List<Journey>();
    }
}
