using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Products
{
    public abstract class Product : ICloneable
    {
        public event EventHandler OnChange;

        private decimal _price;
        private double _weightKg;

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { 
            get { return _price; } 
            set { 
                if(value >= 0)
                {
                    _price = value;
                    Change();
                }
                else throw new ArgumentException("Value can't be less than zero");
            } 
        }

        /// <summary>
        /// Weight of a single unit of product in kg
        /// </summary>
        public double WeightKg
        {
            get { return _weightKg; }
            set
            {
                if(value >= 0)
                {
                    _weightKg = value;
                    Change();
                }
                else throw new ArgumentException("Value can't less than zero");
            }
        }
        public Product()
        {

        }
        public Product(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            WeightKg = product.WeightKg;
        }

        public Product(string name, decimal price, double weightKg)
        {
            Name = name;
            Price = price;
            WeightKg = weightKg;
        }

        public void Change()
        {
            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return $"Name: {Name} \nPrice: {Price} \nWeight(kg): {WeightKg}";
        }

        public override bool Equals(object obj)
        {
            Product other = obj as Product;

            if(other == null) return false;

            return 
                Name == other.Name && 
                Price == other.Price && 
                WeightKg == other.WeightKg;
        }

        public abstract object Clone();
    }
}
