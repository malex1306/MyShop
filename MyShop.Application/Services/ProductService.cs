using MyShop.Application.Services;
using MyShop.Domain.Entities;
using MyShop.Domain.Interfaces;
using MyShop.Domain.ValueObjects.Product;
using MyShop.Infrastructure.Dtos;

namespace MyShop.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task CreateProductAsync(ProductDto dto)
        {
            var product = new Product(
                new ProductName(dto.Name),
                new Money(dto.Price),
                dto.Description,
                Enum.Parse<ProductCategory>(dto.Category),
                dto.StockQuantity);

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto dto)
        {
            var existing = await _productRepository.GetByIdAsync(dto.Id);
            if (existing == null) return;

            existing.UpdateName(new ProductName(dto.Name));
            existing.UpdatePrice(new Money(dto.Price));
            existing.UpdateDescription(dto.Description);
            existing.UpdateCategory(Enum.Parse<ProductCategory>(dto.Category));
            existing.UpdateStockQuantity(dto.StockQuantity);

            await _productRepository.UpdateAsync(existing);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

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
