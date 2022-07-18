using ShopLib.Products.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopLib.Products
{
    public class Diary : IProduct, IExpirable
    {
        #region Properties
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public DateTime ExpirationDate { get; set; }
        #endregion

        #region Constructors
        public Diary()
        {
        }
        public Diary(Diary diaryProduct)
        {
            Name = diaryProduct.Name;
            Price = diaryProduct.Price;
            Weight = diaryProduct.Weight;
            ExpirationDate = diaryProduct.ExpirationDate;
        }
        public Diary(string name, decimal price, double weightKg, DateTime expirationDate)
        {
            Name = name;
            Price = price;
            Weight = weightKg;
            ExpirationDate = expirationDate;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{base.ToString()} \nExpiration Date: {ExpirationDate}";
        }

        public override bool Equals(object obj)
        {
            Diary other = obj as Diary;
            if (other == null) return false;

            return
                ExpirationDate == other.ExpirationDate &&
                base.Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Weight, ExpirationDate);
        }
        public object Clone()
        {
            return new Diary(this);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

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

        
    }
}
