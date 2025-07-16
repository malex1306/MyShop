using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Entities;
using MyShop.Domain.Interfaces;

namespace MyShop.Infrastructure.Persistence.Repositories
{
    public class EfCoreOrderRepository : IOrderRepository
    {
        private readonly MyShopDbContext _context;

        public EfCoreOrderRepository(MyShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Order> getAll()
        {
            return _context.Orders
                .Include(o => o.Items)
                .ToList();
        }

        public Order? getById(int id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}
