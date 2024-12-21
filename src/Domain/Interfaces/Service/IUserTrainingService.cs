using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUserTrainingService
    {
        Task<List<UserTraining>> GetAll();
        Task<UserTraining> GetById(int id);
        Task Create(UserTraining model);
        Task Update(UserTraining model);
        Task Delete(int id);
    }
}