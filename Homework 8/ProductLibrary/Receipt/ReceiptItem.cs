using ProductLibrary.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Receipt
{
    // Buy

    /// <summary>
    /// Line in a receipt
    /// </summary>
    public class ReceiptItem
    {
        public event EventHandler<ReceiptItemEventArgs> OnChange;

        private Product _product;
        private int _quantity;
        private decimal _totalPrice;
        private double _totalWeight;
        
        public Product Product {
            get { return _product; }
            set {
                if(value == null) throw new ArgumentNullException("value");

                if(_product != null)
                {
                    _product.OnChange -= Product_OnChange;
                }
                _product = value;
                // subcribe to new product
                _product.OnChange += Product_OnChange;
                SetNewPriceAndWeight(product: value);
            }
        }

        public int Quantity {
            get { return _quantity; } 
            set {
                if(value < 0) throw new ArgumentOutOfRangeException("value");

                _quantity = value;
                SetNewPriceAndWeight(quantity: value); 
            } 
        }

        public decimal TotalPrice { 
            get { return _totalPrice; } 
            private set 
            {
                decimal oldTotalPrice = TotalPrice;
                double oldTotalWeight = TotalWeight;

                _totalPrice = value;

                if (Product != null && Quantity > 0)
                {
                    Change(oldTotalPrice, oldTotalWeight);
                }
            } 
        }

        public double TotalWeight
        {
            get { return _totalWeight; }
            private set
            {
                decimal oldTotalPrice = TotalPrice;
                double oldTotalWeight = TotalWeight;

                _totalWeight = value;

                if (Product != null && Quantity > 0)
                {
                    Change(oldTotalPrice, oldTotalWeight);
                }
            }
        }

        //public ReceiptItem() : this(new Product(), default)
        //{
        //}
        public ReceiptItem(Product product, int quanity)
        {   
            if(product != null && quanity > 0)
            {
                Product = product;
                Quantity = quanity;

                SetNewPriceAndWeight(Product, Quantity);

                return;
            }

            throw new ArgumentException("Product == null or quantity > 0");
        }

        private void Product_OnChange(object sender, EventArgs e)
        {
            Product changedProduct = sender as Product;

            if (changedProduct != null)
            {
                SetNewPriceAndWeight(changedProduct);
            }
        }

        private void SetNewPriceAndWeight(Product product) 
        {
            TotalPrice = product.Price * Quantity;
            TotalWeight = product.WeightKg * Quantity;
        }

        private void SetNewPriceAndWeight(int quantity)
        {
            TotalPrice = Product.Price * quantity;
            TotalWeight = Product.WeightKg * quantity;
        }

        private void SetNewPriceAndWeight(Product product, int quantity)
        {
            TotalPrice = product.Price * quantity;
            TotalWeight = product.WeightKg * quantity;
        }

        public class ReceiptItemEventArgs : EventArgs
        {
            public decimal OldTotalPrice { get; set; }

            public double OldTotalWeight { get; set; }
            public ReceiptItemEventArgs(decimal oldTotalPrice, double oldTotalWeight)
            {
                OldTotalPrice = oldTotalPrice;
                OldTotalWeight = oldTotalWeight;
            }
        }

        public void Change(decimal oldTotalPrice, double oldTotalWeight)
        {
            OnChange?.Invoke(this, new ReceiptItemEventArgs(oldTotalPrice, oldTotalWeight));
        }
    }
}
