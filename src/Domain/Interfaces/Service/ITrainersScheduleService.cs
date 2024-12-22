using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface ITrainersScheduleService
    {
        Task<List<TrainersSchedule>> GetAll();
        Task<TrainersSchedule> GetById(int id);
        Task Create(TrainersSchedule model);
        Task Update(TrainersSchedule model);
        Task Delete(int id);
    }
}
