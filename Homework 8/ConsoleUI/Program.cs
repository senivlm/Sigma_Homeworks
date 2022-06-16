using EnergyAccounting;
using ProductLibrary.Products;
using ProductLibrary.Storage;
using System;
using WebsiteVisiting;
using static ProductLibrary.Utils;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1
            Report r1 = new Report(@"C:\Users\Иван\source\repos\Sigma_homeworks\Homework 8\report_1.txt");
            Report r2 = new Report(@"C:\Users\Иван\source\repos\Sigma_homeworks\Homework 8\report_2.txt");

            Report r3 = r1 + r2;
            r1 -= r2;
            r2 += r1;

            Console.WriteLine();
            #endregion

            #region Task 2

            string visitorsFile = @"C:\Users\Иван\source\repos\Sigma_homeworks\Homework 8\weekly_visiting_test1.txt";
            WeekVisitings weekVisitings = new WeekVisitings(visitorsFile);

            // Для кожного ip вкажіть кількість відвідувань на тиждень,
            // найбільш популярний день тижня,
            // найбільш популярний відрізок часу довжиною в одну годину.
            foreach (var visitor in weekVisitings.Visitors)
            {
                Console.WriteLine($"{visitor.IP} - {visitor.Visits.Count} - {visitor.MostPopularDay()} - {visitor.MostPopularHour()}");
            }

            // Знайдіть також найбільш популярний відрізок часу в добі довжиною одну годину в цілому для сайту. 
            Console.WriteLine();
            Console.WriteLine(weekVisitings.MostPopularHour());

            Console.WriteLine();
            

            #endregion

            #region Task 3
            string products1 = @"C:\Users\Иван\source\repos\Sigma_homeworks\Homework 8\products_1.txt";
            string products2 = @"C:\Users\Иван\source\repos\Sigma_homeworks\Homework 8\products_2.txt";

            Storage s1;
            (s1, _) = products1.LoadFile().ConvertToStorage();
            Storage s2;
            (s2, _) = products2.LoadFile().ConvertToStorage();

            // a.	Товари є в першому складі і немає в другому.
            Storage exLeftJoin = s1.ExclusiveLeftJoin(s2);

            // b.	Товари, які  є спільними в обох складах.
            Storage innerJoin = s1.InnerJoin(s2);

            // c.	Спільний список товарів, які є на обох складах, без повторів елементів.
            Storage fullJoinDistinct = s1.FullJoinDistinct(s2);

            Console.WriteLine();
            #endregion
        }
    }
}