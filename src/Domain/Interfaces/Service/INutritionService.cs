using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface INutritionService
    {
        Task<List<Nutrition>> GetAll();
        Task<Nutrition> GetById(int id);
        Task Create(Nutrition model);
        Task Update(Nutrition model);
        Task Delete(int id);
    }
}