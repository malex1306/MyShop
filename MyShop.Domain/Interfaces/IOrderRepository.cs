using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Entities;

namespace MyShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> getAll();
        Order? getById(int id);
        void Add(Order order);
    }
}
