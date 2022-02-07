using System;
using Car.Data.Enums;
using Point = Car.Data.Entities.Point;

namespace Car.Domain.Dto
{
    public class RequestDto
    {
        public int Id { get; set; }

        public Point? From { get; set; }

        public Point? To { get; set; }

        public DateTime DepartureTime { get; set; }

        public FeeType Fee { get; set; }

        public int PassengersCount { get; set; }

        public int UserId { get; set; }
    }
}
