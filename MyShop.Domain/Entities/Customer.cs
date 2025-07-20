using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Order> Orders { get; set; } = new List<Order>();

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }
        
        public decimal GetTotalSpent()
        {
            return Orders.Sum(o => o.TotalAmount);
        }
    }
}
