using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Order
{
    public partial class Order : IEnumerable<ProductStock>, IList<ProductStock>
    {
        private List<ProductStock> _products;

        #region Properties
        public decimal Price { get; set; }

        public double Weight { get; set; }
        #endregion

        public void ChangePricesDueToExpiration()
        {
            foreach (var stock in _products)
            {
                if (stock.Product is Diary)
                {
                    ((Diary)stock.Product).ChangePriceDueToExpiration();
                }
            }
        }
    }
}
