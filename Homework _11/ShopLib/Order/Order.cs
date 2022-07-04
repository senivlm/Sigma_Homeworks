using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Order
{
    public partial class Order : IProductContainer<IProduct> 
    {
        private Dictionary<IProduct, int> _productsStock;

        #region Properties
        public decimal Price { get; set; }

        public double Weight { get; set; }
        #endregion

        public void ChangePricesDueToExpiration()
        {
            foreach (var stock in _productsStock)
            {
                if (stock.Key is Diary)
                {
                    ((Diary)stock.Key).ChangePriceDueToExpiration();
                }
            }
        }
    }
}
