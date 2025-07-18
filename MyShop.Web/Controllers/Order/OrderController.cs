using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Interfaces;
using MyShop.Infrastructure.Dtos;
using MyShop.Infrastructure.Services;

namespace MyShop.Web.Controllers.Order
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _services;
        private readonly IOrderRepository _repository;

        public OrderController(OrderServices services, IOrderRepository repository)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_repository.getAll());
        }

        [HttpPost]
        public IActionResult NewOrder([FromForm] CreateOrderDto dto)
        {
            var order = _services.CreateOrder(dto);
            _repository.Add(order);

            return Ok(new
            {
                Orginal = order.TotalAmount,
                Order = order
            });
        }

    }
}
