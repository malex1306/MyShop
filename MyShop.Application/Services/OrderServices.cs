using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Entities;
using MyShop.Domain.Interfaces;
using MyShop.Domain.ValueObjects;
using MyShop.Domain.ValueObjects.Order;
using MyShop.Infrastructure.Dtos;

namespace MyShop.Infrastructure.Services
{
    public class OrderServices
    {
        private readonly IOrderRepository _repository;

        public OrderServices(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Order CreateOrder(CreateOrderDto dto)
        {
            var order = new Order
            {
                Id = 0, // Assuming Id is auto-generated
                CustomerName = dto.CustomerName ?? throw new ArgumentNullException(nameof(dto.CustomerName)),
            };

            foreach (var itm in dto.Item)
            {
                var item = new OrderItem(itm.ProductName, itm.Price, itm.Quantity);
                order.AddItem(item);
            }
            return order;
        }

    }
}
