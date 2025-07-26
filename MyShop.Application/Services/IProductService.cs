using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Entities;
using MyShop.Infrastructure.Dtos;

namespace MyShop.Application.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductDto dto);
        Task UpdateProductAsync(ProductDto dto);
        Task DeleteProductAsync(int id);
    }
}
