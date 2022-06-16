using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace EnergyAccounting
{
    public class Report : ICloneable
    {
        public byte Quarter { get; set; }
        public List<FlatReport> FlatReports { get; set; } = new List<FlatReport>();
        public Report()
        {

        }
        public Report(Report report)
        {
            Quarter = report.Quarter;

            foreach (var fr in report.FlatReports)
            {
                FlatReports.Add((FlatReport)fr.Clone());
            }

        }
        public Report(string filePath)
        {
            if(filePath == null) 
                throw new ArgumentNullException(nameof(filePath));

            try
            {
                (FlatReports, Quarter) = TextProcessor.LoadFile(filePath).ConvertToReports();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Report(List<FlatReport> flatReports, byte quarter)
        {
            FlatReports = flatReports;
            Quarter = quarter;
        }

        public static Report operator +(Report r1, Report r2)
        {
            Report result = new Report();

            foreach (var fr in r1.FlatReports)
            {
                result.FlatReports.Add((FlatReport)fr.Clone());
            }
            foreach (var fr in r2.FlatReports)
            {
                result.FlatReports.Add((FlatReport)fr.Clone());
            }

            return result;
        }
        public static Report operator -(Report r1, Report r2)
        {
            Report result = new Report();

            result.FlatReports = r1.FlatReports.Except(r2.FlatReports, new FlatReportComparer()).ToList();

            return result;
        }
        public void PrintToFile(string filePath)
        {
            // build string array for each line
            var reportTable = TextProcessor.GetReportTable(this);

            // write it into the file
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            File.WriteAllLines(filePath, reportTable);
        }
        public FlatReport GetBiggestBill()
        {
            decimal max = 0;
            FlatReport result = null;

            foreach (var fr in FlatReports)
            {
                if (fr.BillsSum > max)
                {
                    max = fr.BillsSum;
                    result = fr;
                }
            }
            return result;
        }

        public DateTime GetLastDate()
        {
            DateTime last = DateTime.MinValue;

            foreach (var fr in FlatReports)
            {
                foreach (var date in fr.Dates)
                {
                    if(date > last)
                        last = date;
                }
            }
            return last;
        }

        public object Clone()
        {
            return new Report(this);
        }


    }
}
