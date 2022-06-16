using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteVisiting
{
    internal static class TextProcessor
    {
        public static List<Visitor> ConvertToVisitors(this List<string> lines)
        {
            List<Visitor> result = new List<Visitor>();
            // IPs that are already added to result
            // IP can have multiple visiting timestamps 
            // but there can only be one IP in the collection
            List<string> usedIPs = new List<string>();

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                VisitTime visitingTime;
                try
                {
                    visitingTime = CreateVisitingTime(line, data);
                }
                catch (Exception)
                {

                    throw;
                }

                if (usedIPs.Contains(data[0]))
                {
                    result.Where(x => x.IP == data[0]).First().Visits.Add(visitingTime);
                }
                else
                {
                    Visitor visitor = new Visitor(data[0]);
                    visitor.Visits.Add(visitingTime);
                    result.Add(visitor);

                    usedIPs.Add(data[0]);
                }
            }

            
            return result;
        }

        private static VisitTime CreateVisitingTime(string line, string[] data)
        {
            string[] timeData = data[1].Split(':');
            TimeOnly time;
            WeekDays day;
            try
            {
                time = new TimeOnly(int.Parse(timeData[0]), int.Parse(timeData[1]), int.Parse(timeData[2]));
                day = (WeekDays)Enum.Parse(typeof(WeekDays), data[2]);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Could not parse data on line: {line}\nError message: {ex.Message}");
            }

            return new VisitTime(time, day);
        }
    }
}
