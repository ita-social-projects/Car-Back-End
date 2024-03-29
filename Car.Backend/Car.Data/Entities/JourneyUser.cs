﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Data.Entities
{
    public class JourneyUser : IEntity
    {
        public int JourneyId { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public Journey? Journey { get; set; }

        public bool WithBaggage { get; set; }

        public int PassangersCount { get; set; }
    }
}
