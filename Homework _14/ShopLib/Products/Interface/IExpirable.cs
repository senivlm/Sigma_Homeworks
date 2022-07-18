using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Products.Interface
{
    public interface IExpirable : IProduct
    {
        public DateTime ExpirationDate { get; set; }
        // не впевнений як це зробити
        // я хочу щоб саме клас мав цей метод
        public bool IsExpired()
        {
            return ExpirationDate < DateTime.Today;
        }
    }
}
