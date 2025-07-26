using System;
using MyShop.Domain.ValueObjects.Product;
namespace MyShop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public ProductName Name { get; private set; }
        public Money Price { get; private set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public int StockQuantity { get; private set; }

        protected Product() { }

        public Product(ProductName name, Money price, string? description, ProductCategory category, int stockQuantity)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Category = category;
            StockQuantity = stockQuantity >= 0 ? stockQuantity : throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be negative.");
        }

        

        public void UpdateName(ProductName name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdatePrice(Money price)
        {
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public void UpdateDescription(string? description)
        {
            Description = description ?? string.Empty;
        }

        public void UpdateCategory(ProductCategory category)
        {
            Category = category;
        }

        public void UpdateStockQuantity(int stockQuantity)
        {
            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be negative.");

            StockQuantity = stockQuantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Nicht genug Lagerbestand.");

            StockQuantity -= quantity;
        }
    }
}
