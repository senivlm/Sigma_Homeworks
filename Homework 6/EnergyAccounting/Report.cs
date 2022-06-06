using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static EnergyAccounting.TextProcessor;

namespace EnergyAccounting
{
    public class Report
    {
        public int Quarter { get; set; }
        public List<FlatReport> FlatReports { get; set; }
        public Report()
        {

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
        public Report(List<FlatReport> flatReports, int quarter)
        {
            FlatReports = flatReports;
            Quarter = quarter;
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
    }
}
