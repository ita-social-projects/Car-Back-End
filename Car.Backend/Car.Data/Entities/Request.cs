using System;
using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Request : IEntity
    {
        public int Id { get; set; }

        public Point From { get; set; }

        public Point To { get; set; }

        public DateTime DepartureTime { get; set; }

        public FeeType Fee { get; set; }

        public int PassengersCount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
