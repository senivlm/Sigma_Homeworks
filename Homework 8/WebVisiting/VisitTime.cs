using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteVisiting
{
    public record VisitTime
    {
        public TimeOnly Time { get; set; }

        public WeekDays Day { get; set; }

        public VisitTime()
        {

        }
        public VisitTime(TimeOnly time, WeekDays day)
        {
            Time = time;
            Day = day;
        }
    }

    public enum WeekDays : byte
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7,
    }
}
