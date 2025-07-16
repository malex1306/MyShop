using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.ValueObjects;

namespace MyShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

    }
}
