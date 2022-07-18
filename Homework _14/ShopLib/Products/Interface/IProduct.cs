using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopLib.Products.Interface
{
    public interface IProduct : ICloneable, ISerializable
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
