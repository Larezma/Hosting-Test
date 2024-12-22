using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAll();
        Task<Schedule> GetById(int id);
        Task Create(Schedule model);
        Task Update(Schedule model);
        Task Delete(int id);
    }
}
