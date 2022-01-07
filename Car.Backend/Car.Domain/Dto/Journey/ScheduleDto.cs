﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Enums;

namespace Car.Domain.Dto.Journey
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public WeekDays Days { get; set; }

        public Data.Entities.Journey? Journey { get; set; }
    }
}
