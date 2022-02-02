using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Data.Entities
{
    public class UserStatistic : IEntity
    {
        public int Id { get; set; }

        public int TotalKm { get; set; }

        public int PassangerJourneysAmount { get; set; }

        public int DriverJourneysAmount { get; set; }

        public User? User { get; set; }
    }
}
