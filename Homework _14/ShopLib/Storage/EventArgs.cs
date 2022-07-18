using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage
{
    public class OnAddingExpiredEventArgs : EventArgs
    {
        public IExpirable ExpiredProduct { get; set; }
        public OnAddingExpiredEventArgs(IExpirable product)
        {
            ExpiredProduct = product;
        }
    }
}
