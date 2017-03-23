using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lines = new List<CartLine>();
        public IList<CartLine> Lines { get { return lines.AsReadOnly(); } }

        public void AddItem(Product product, int quantity)
        {
            var line = lines.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line == null)
                lines.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public decimal ComputeTotalValue()
        {
            return lines.Sum(l => l.Product.Price * l.Quantity);
        }

        public void Clear() 
        { 
            lines.Clear();
        }

        public void RemoveLine(Product product)
        {
            lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}