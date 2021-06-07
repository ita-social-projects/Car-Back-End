using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Domain.Dto
{
    public class UserPreferencesDto
    {
        public int Id { get; set; }

        public bool DoAllowSmoking { get; set; }

        public bool DoAllowEating { get; set; }

        public string Comments { get; set; }
    }
}
