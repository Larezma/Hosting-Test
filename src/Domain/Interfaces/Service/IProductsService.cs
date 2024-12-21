using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IProductsService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product model);
        Task Update(Product model);
        Task Delete(int id);
    }
}
