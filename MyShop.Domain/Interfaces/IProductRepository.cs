using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Entities;

namespace MyShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
        Product? GetById(int id);
        List<Product> GetAll();
    }
}
