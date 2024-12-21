using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IAchievementsService
    {
        Task<List<Achievement>> GetAll();
        Task<Achievement> GetById(int id);
        Task Create(Achievement model);
        Task Update(Achievement model);
        Task Delete(int id);
    }
}
