using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteVisiting
{
    public class Visitor
    {
        public string IP { get; init; }

        public List<VisitTime> Visits { get; } 
        public Visitor()
        {
            Visits = new List<VisitTime>();
        }
        public Visitor(string ip) : this()
        {
            IP = ip;
        }

        public WeekDays MostPopularDay()
        {
            uint[] daysCount = new uint[7];
            foreach (var item in Visits)
            {
                switch (item.Day)
                {
                    case WeekDays.Monday: daysCount[0]++; break;
                    case WeekDays.Tuesday: daysCount[1]++; break;
                    case WeekDays.Wednesday: daysCount[2]++; break;
                    case WeekDays.Thursday: daysCount[3]++; break;
                    case WeekDays.Friday: daysCount[4]++; break;
                    case WeekDays.Saturday: daysCount[5]++; break;
                    case WeekDays.Sunday: daysCount[6]++; break;
                }
            }

            var (_, index) = daysCount.Select((n, i) => (n, i)).Max();

            switch (index)
            {
                case 0:
                    return WeekDays.Monday;
                case 1:
                    return WeekDays.Tuesday;
                case 2:
                    return WeekDays.Wednesday;
                case 3:
                    return WeekDays.Thursday;
                case 4:
                    return WeekDays.Friday;
                case 5:
                    return WeekDays.Saturday;
                case 6:     
                    return WeekDays.Sunday;
                default:
                    throw new Exception();
            }
        }

        public int MostPopularHour()
        {
            List<int> hours = new List<int>();

            foreach (var visit in Visits)
            {
                hours.Add(visit.Time.Hour);
            }

            int maxCount = 0;
            int elementHavingMaxFreq = 0;
            for (int i = 0; i < hours.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < hours.Count; j++)
                {
                    if (hours[i] == hours[j])
                        count++;
                }

                if (count > maxCount)
                {
                    maxCount = count;
                    elementHavingMaxFreq = hours[i];
                }

            }

            return elementHavingMaxFreq;
        }
    }
}
