using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Products
{
    public sealed class Meat : Product
    {
        public MeatQuality Quality { get; set; }

        public MeatCategory Category { get; set; }

        public Meat()
        {

        }
        public Meat(string name, decimal price, double weightKg, MeatQuality quality, MeatCategory category) 
            : base(name, price, weightKg)
        {
            Quality = quality;
            Category = category;
        }

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

        public override string ToString()
        {
            return $"{base.ToString()} \nQuality: {Quality} \nType: {Category}";
        }

        public override bool Equals(object obj)
        {
            Meat other = obj as Meat;

            if(other == null) return false;

            return
                Quality == other.Quality &&
                Category == other.Category &&
                base.Equals(other);
        }

        
    }
}
