using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Entities;
using MyShop.Domain.Interfaces;
using MyShop.Domain.ValueObjects.Product;
using MyShop.Infrastructure.Dtos;

namespace MyShop.Infrastructure.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return products.Select(MapToDto).ToList();
        }

        public ProductDto? GetProductById(int id)
        {
            var product = _productRepository.GetById(id);
            return product == null ? null : MapToDto(product);
        }

        public void AddProduct(ProductDto dto)
        {
            var product = new Product(
                new ProductName(dto.Name),
                new Money(dto.Price),
                dto.Description,
                Enum.Parse<ProductCategory>(dto.Category),
                dto.StockQuantity);

            _productRepository.Add(product);
        }

        //Mapping
        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name.Value,
                Price = product.Price.Amount,
                Description = product.Description,
                Category = product.Category.ToString(),
                StockQuantity = product.StockQuantity
            };
        }
    }
}
