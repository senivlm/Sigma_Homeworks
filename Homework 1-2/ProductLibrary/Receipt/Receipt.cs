using ProductLibrary.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Receipt
{
    public class Receipt : IEnumerable<ReceiptItem>
    {
        private List<ReceiptItem> _receiptItems = new List<ReceiptItem>();

        public decimal TotalPrice { get; private set; }

        public double TotalWeight { get; private set; }

        public ReceiptItem this[int index]
        {
            get { return _receiptItems[index]; }
            set {
                TotalPrice -= _receiptItems[index].TotalPrice;
                TotalWeight -= _receiptItems[index].TotalWeight;

                TotalPrice += value.TotalPrice;
                TotalWeight += value.TotalWeight;
                _receiptItems[index].OnChange -= ReceiptItem_OnChange;

                _receiptItems[index] = value;
            }
        }

        public void Add(ReceiptItem item)
        {
            _receiptItems.Add(item);
            item.OnChange += ReceiptItem_OnChange;
            TotalPrice += item.TotalPrice;
            TotalWeight += item.TotalWeight;
        }

        public void Remove(ReceiptItem item)
        {
            _receiptItems.Remove(item);
            item.OnChange -= ReceiptItem_OnChange;
            TotalPrice -= item.TotalPrice;
            TotalWeight -= item.TotalWeight;
        }

        public void ChangePricesDueToExpiration()
        {
            foreach (var item in _receiptItems)
            {
                if(item.Product is DiaryProduct)
                {
                    ((DiaryProduct)item.Product).ChangePriceDueToExpiration();
                }
            }
        }

        private void ReceiptItem_OnChange(object sender, ReceiptItem.ReceiptItemEventArgs e)
        {
            ReceiptItem changedItem = sender as ReceiptItem;

            if (changedItem != null)
            {
                TotalPrice -= e.OldTotalPrice;
                TotalWeight -= e.OldTotalWeight;

                TotalPrice += changedItem.TotalPrice;
                TotalWeight += changedItem.TotalWeight;
            }
        }

        public IEnumerator<ReceiptItem> GetEnumerator()
        {
            for (int i = 0; i < _receiptItems.Count; i++)
                yield return _receiptItems[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
