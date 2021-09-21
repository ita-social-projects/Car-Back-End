using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Data.Enums
{
    [Flags]
    public enum WeekDays
    {
        /// <summary>
        /// Monday.
        /// </summary>
        Monday = 1,

        /// <summary>
        /// Tuesday.
        /// </summary>
        Tuesday = 1 << 1,

        /// <summary>
        /// Wednesday.
        /// </summary>
        Wednesday = 1 << 2,

        /// <summary>
        /// Thursday.
        /// </summary>
        Thursday = 1 << 3,

        /// <summary>
        /// Friday.
        /// </summary>
        Friday = 1 << 4,

        /// <summary>
        /// Saturday.
        /// </summary>
        Saturday = 1 << 5,

        /// <summary>
        /// Sunday.
        /// </summary>
        Sunday = 1 << 6,
    }
}
