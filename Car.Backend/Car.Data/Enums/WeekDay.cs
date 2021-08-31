using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Data.Enums
{
    [Flags]
    public enum WeekDay
    {
        /// <summary>
        /// Monday.
        /// </summary>
        Monday = 1,

        /// <summary>
        /// Tuesday.
        /// </summary>
        Tuesday = 2,

        /// <summary>
        /// Wednesday.
        /// </summary>
        Wednesday = 4,

        /// <summary>
        /// Thursday.
        /// </summary>
        Thursday = 8,

        /// <summary>
        /// Friday.
        /// </summary>
        Friday = 16,

        /// <summary>
        /// Saturday.
        /// </summary>
        Saturday = 32,

        /// <summary>
        /// Sunday.
        /// </summary>
        Sunday = 64,
    }
}
