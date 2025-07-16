using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Dtos
{
    public class CreateOrderDto
    {
        public string? CustomerName { get; set; }
        public List<OrderItemDto> Item { get; set; }
    }

    public class OrderItemDto
    {
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    } 
}
