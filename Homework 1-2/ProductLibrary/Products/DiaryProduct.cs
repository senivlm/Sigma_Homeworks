using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Products
{
    public sealed class DiaryProduct : Product
    {
        public DateTime ExpirationDate { get; set; }

        public DiaryProduct() : this(default, default, default, default)
        {
        }
        public DiaryProduct(string name, decimal price, double weightKg, DateTime expirationDate) : base(name, price, weightKg)
        {
            ExpirationDate = expirationDate;
        }

        public void ChangePriceDueToExpiration()
        {
            Price *= GetDiscount();
        }

        // TODO const
        private decimal GetDiscount()
        {
            // 25%
            if (ExpirationDate.Day == DateTime.Today.AddDays(-2).Day)
                return .75m;
            // 50%
            if (ExpirationDate.Day == DateTime.Today.AddDays(-1).Day)
                return .5m;
            // 75%
            if (ExpirationDate.Day == DateTime.Today.Day)
                return .25m;

            // 0%
            return 1;
        }
        public override string ToString()
        {
            return $"{base.ToString()} \nExpiration Date: {ExpirationDate}";
        }

        public override bool Equals(object obj)
        {
            DiaryProduct other = obj as DiaryProduct;
            if (other == null) return false;

            return 
                ExpirationDate == other.ExpirationDate &&
                base.Equals(other);
        }
    }
}
