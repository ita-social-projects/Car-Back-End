using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Domain.Models.Journey
{
    public class ScheduleTimeModel
    {
        public ScheduleModel? ScheduleModel { get; set; }

        public bool IsDepartureTimeValid { get; set; }
    }
}
