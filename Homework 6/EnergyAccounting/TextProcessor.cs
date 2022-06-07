using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EnergyAccounting
{
    internal static class TextProcessor
    {
        private static string br = "|-------------------------------------------------------------------------------------------------------|";
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static (List<FlatReport>, int quarter) ConvertToReports(this List<string> lines)
        {
            List<FlatReport> flatReports = new List<FlatReport>();

            // lines
            // numOfFlats, quarter
            // flatNum, lastName, kwt, kwt, kwt, date, date, date
            // so on

            string[] firstLine = lines[0].Split(Config.FILE_SEPARATOR);
            int numOfFlats = int.Parse(firstLine[0]);
            
            if(numOfFlats != lines.Count - 1)
                throw new InvalidDataException("Difference between number of flats and number of lines");

            int quarter = int.Parse(firstLine[1]);
            switch (quarter)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    break;
                default:
                    throw new InvalidDataException("Invalid quarter on line 1");
            }
                

            for (int i = 1; i < lines.Count; i++)
            {
                FlatReport r = new FlatReport();

                string[] line = lines[i].Split(Config.FILE_SEPARATOR);

                if(line.Length < 5 + Config.DATES_PER_QUARTER)
                    throw new InvalidDataException($"Not enough data on line {i+1}");
                if (line.Length > 5 + Config.DATES_PER_QUARTER)
                    throw new InvalidDataException($"Superfluous data on line {i+1}");

                try
                {
                    r.FlatNumber = line[0];
                    r.LastName = line[1].Trim();
                    for (int j = 0; j < Config.DATES_PER_QUARTER; j++)
                    {
                        r.Consumed[j] = int.Parse(line[2 + j]);
                    }
                    for (int j = 0; j < Config.DATES_PER_QUARTER; j++)
                    {
                        r.Dates[j] = DateTime.Parse(line[5 + j]);
                    }
                }
                catch (Exception)
                {
                    throw new InvalidDataException($"Couldn't parse data on line {i+1}");
                }

                r.SetBills(Config.GrnPerKwt);
                flatReports.Add(r);
            }

            return (flatReports, quarter);
        }

        public static List<string> GetReportTable(Report report)
        {
            List<string> result = new List<string>();

            // build header
            var header = GetTableHeader(report.Quarter);

            // fill with data
            var data = GetTableData(report);

            result.AddRange(header);
            result.AddRange(data);

            return result;
        }

        private static List<string> GetTableHeader(int quarter)
        {
            int firstMonth;
            switch (quarter)
            {
                case 1:
                    firstMonth = 1;
                    break;
                case 2:
                    firstMonth = 4;
                    break;
                case 3:
                    firstMonth = 7;
                    break;
                case 4:
                    firstMonth = 10;
                    break;
                default:
                    throw new InvalidDataException("Invalid quarter number");
            }

            List<string> monthNames = GetMonths(firstMonth);

            List<string> header = new List<string>();

            header.Add(br);
            header.Add($"| {TabSmall("#")}| {Tab("Прізвище")}| {TabBig(@"Знято \ Показник \ Сума")}| {Tab("Всього")} {Tab("")}|");
            header.Add($"| {TabSmall("")}| {Tab("")}|-------------------------------------------------------------------------------|");
            header.Add($"| {TabSmall("")}| {Tab("")}| {Tab(monthNames[0])}| {Tab(monthNames[1])}| {Tab(monthNames[2])}| {Tab("kWt")}| {Tab("Грн")}|");
            header.Add(br);

            return header;
        }

        private static List<string> GetMonths(int firstMonth)
        {
            CultureInfo current = CultureInfo.CurrentCulture;

            TextInfo ukrTextInfo = new CultureInfo("uk-UA", false).TextInfo;
            CultureInfo.CurrentCulture = new CultureInfo("uk-UA", false);

            List<string> monthNames = new List<string>();
            for (int i = firstMonth; i < firstMonth + Config.DATES_PER_QUARTER; i++)
            {
                monthNames.Add(ukrTextInfo.ToTitleCase(DateTimeFormatInfo.CurrentInfo.GetMonthName(i)));
            }

            CultureInfo.CurrentCulture = current;

            return monthNames;
        }

        private static List<string> GetTableData(Report report)
        {
            if (report == null) 
                throw new ArgumentNullException();

            List<string> tableData = new List<string>();

            foreach (var fr in report.FlatReports)
            {
                tableData.Add($"| {TabSmall(fr.FlatNumber)}| {Tab(fr.LastName)}| {Tab(fr.Dates[0].Date.ToShortDateString())}| {Tab(fr.Dates[1].Date.ToShortDateString())}| {Tab(fr.Dates[2].Date.ToShortDateString())}| {Tab(fr.ConsumedSum.ToString() + "kWt")}| {Tab(fr.BillsSum.ToString() + "грн")}|");
                tableData.Add($"| {TabSmall("")}| {Tab("")}| {Tab(fr.Consumed[0].ToString() + "kWt")}| {Tab(fr.Consumed[1].ToString() + "kWt")}| {Tab(fr.Consumed[2].ToString() + "kWt")}| {Tab("")}| {Tab("")}|");
                tableData.Add($"| {TabSmall("")}| {Tab("")}| {Tab(fr.Bills[0].ToString() + "грн")}| {Tab(fr.Bills[1].ToString() + "грн")}| {Tab(fr.Bills[2].ToString() + "грн")}| {Tab("")}| {Tab("")}|");
                tableData.Add(br);
            }
            
            return tableData;
        }

        private static string Tab(string str)
        {
            if (str.Length > 5)
                str += "\t";
            else
                str += "\t\t";

            return str;
        }

        private static string TabSmall(string str)
        {
            if (str.Length < 6)
                str += "\t";

            return str;
        }

        private static string TabBig(string str)
        {
            if (str.Length < 24)
                str += "\t\t\t";

            return str;
        }
    }
}
