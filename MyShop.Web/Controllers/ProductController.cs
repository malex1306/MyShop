﻿using Microsoft.AspNetCore.Mvc;
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
            return Ok(_productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto)
        {
            _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}