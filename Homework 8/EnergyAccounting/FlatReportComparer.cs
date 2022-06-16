using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyAccounting
{
    internal class FlatReportComparer : IEqualityComparer<FlatReport>
    {
        public bool Equals(FlatReport x, FlatReport y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) 
                return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(FlatReport obj)
        {
            //return (obj == null) ? 0 : obj.GetHashCode();

            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the LastName field if it is not null.
            int hashFlatReportLastName = obj.LastName.GetHashCode();//obj.LastName == null ? 0 : obj.LastName.GetHashCode();

            //Get hash code for the FlatNumber field.
            int hashFlatReportFlatNumber = obj.FlatNumber.GetHashCode();

            //Calculate the hash code for the product.
            return hashFlatReportLastName ^ hashFlatReportFlatNumber;
        }
    }
}
