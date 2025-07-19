using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Dtos;
using MyShop.Infrastructure.Services;

namespace MyShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> GetAll()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _productService.GetProductById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto)
        {
            _productService.AddProduct(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}