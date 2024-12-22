using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface ITrainerService
    {
        Task<List<Trainer>> GetAll();
        Task<Trainer> GetById(int id);
        Task Create(Trainer model);
        Task Update(Trainer model);
        Task Delete(int id);
    }
}
