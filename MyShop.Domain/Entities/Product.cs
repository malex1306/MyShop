using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.ValueObjects.Product;

namespace MyShop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public ProductName Name { get; private set; }
        public Money Price { get; set; }
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public int StockQuantity { get; set; }

        public Product(ProductName name, Money price, string? description, ProductCategory category, int stockQuantity)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Description = description;
            Category = category ?? throw new ArgumentNullException(nameof(category));
            StockQuantity = stockQuantity >= 0 ? stockQuantity : throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be negative.");
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity > StockQuantity)
            {
                throw new InvalidOperationException("Nicht genug Lagerbestand.");

                StockQuantity -= quantity;
            }
        }
    }
}
