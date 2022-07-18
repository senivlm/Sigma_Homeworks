using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopLib.Products
{
    [Serializable]
    public class Meat : IProduct
    {
        #region Properties
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public MeatCategory Category { get; set; }

        public MeatQuality Quality { get; set; }
        #endregion

        #region Constuctors
        public Meat()
        {

        }
        public Meat(Meat meat)
        {
            Name = meat.Name;
            Price = meat.Price;
            Weight = meat.Weight;
            Quality = meat.Quality;
            Category = meat.Category;
        }
        public Meat(string name, decimal price, double weightKg, MeatQuality quality, MeatCategory category)
        {
            Name = name;
            Price = price;
            Weight = weightKg;
            Quality = quality;
            Category = category;
        }

        public Meat(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Price = (decimal)info.GetValue("Price", typeof(decimal));
            Weight = (double)info.GetValue("Weight", typeof(double));
            Quality = (MeatQuality)info.GetValue("Quality", typeof(MeatQuality));
            Category = (MeatCategory)info.GetValue("Category", typeof(MeatCategory));
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{base.ToString()} \nQuality: {Quality} \nType: {Category}";
        }

        public override bool Equals(object obj)
        {
            Meat other = obj as Meat;

            if (other == null) return false;

            return
                Quality == other.Quality &&
                Category == other.Category &&
                base.Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Weight, Category, Quality);
        }
        public object Clone()
        {
            return new Meat(this);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Price", Price);
            info.AddValue("Weight", Weight);
            info.AddValue("Quality", Quality);
            info.AddValue("Category", Category);
        }
        #endregion

        #region Enums
        public enum MeatQuality : byte
        {
            First = 1,
            Second = 2,
        }

        public enum MeatCategory : byte
        {
            Lamb = 1,
            Veal = 2,
            Pork = 3,
            Chicken = 4
        } 
        #endregion
    }
}
