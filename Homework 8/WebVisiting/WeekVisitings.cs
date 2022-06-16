using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteVisiting
{
    public class WeekVisitings
    {
        public List<Visitor> Visitors { get; } = new List<Visitor>();

        public WeekVisitings(string filePath)
        {
            try
            {
                Visitors = filePath.LoadFile().ConvertToVisitors();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int MostPopularHour()
        {
            List<int> hours = new List<int>();

            foreach (var visitor in Visitors)
            {
                hours.Add(visitor.MostPopularHour());
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
