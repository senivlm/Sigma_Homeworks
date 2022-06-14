using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnergyAccounting
{// Все чітко!
    // TODO back to internal
    public class FlatReport
    {
        public string FlatNumber { get; set; } // 98 102a 102b 55/2

        /// <summary>
        /// Last name of the flat owner
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Energy consumed per each respective month
        /// </summary>
        public int[] Consumed { get; private set; }

        public DateTime[] Dates { get; private set; }

        public decimal[] Bills { get; private set; }

        public int ConsumedSum { 
            get 
            {
                return Consumed.Sum();
            }
        }

        public decimal BillsSum { 
            get 
            {
                return Bills.Sum();
            }
        }

        public FlatReport()
        {
            Consumed = new int[Config.DATES_PER_QUARTER];
            Dates = new DateTime[Config.DATES_PER_QUARTER];
            Bills = new decimal[Config.DATES_PER_QUARTER];
        }
        public FlatReport(string flatNum, string lastName) : this()
        {
            FlatNumber = flatNum;
            LastName = lastName;
        }

        public void SetBills(decimal perKwt)
        {
            Bills[0] = Consumed[0] * perKwt;
            Bills[1] = Consumed[1] * perKwt;
            Bills[2] = Consumed[2] * perKwt;
        }
    }
}
