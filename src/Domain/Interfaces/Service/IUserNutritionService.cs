using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUserNutritionService
    {
        Task<List<UserNutrition>> GetAll();
        Task<UserNutrition> GetById(int id);
        Task Create(UserNutrition model);
        Task Update(UserNutrition model);
        Task Delete(int id);
    }
}
