using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface ITrainingService
    {
        Task<List<Training>> GetAll();
        Task<Training> GetById(int id);
        Task Create(Training model);
        Task Update(Training model);
        Task Delete(int id);
    }
}
