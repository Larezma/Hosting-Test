using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;

namespace BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ScheduleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Schedule>> GetAll()
        {
            return await _repositoryWrapper.Schedule.FindALL();
        }

        public async Task<Schedule> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var schedule = await _repositoryWrapper.Schedule
                .FindByCondition(x => x.ScheduleId == id);
            return schedule.First();
        }

        public async Task Create(Schedule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.TrainingId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _) || string.IsNullOrEmpty(model.TrainingType) || string.IsNullOrEmpty(model.DayOfWeek) || !DateTime.TryParse(model.StartTime.ToString(), out _) || !DateTime.TryParse(model.EndTime.ToString(), out _))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Schedule.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Schedule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.ScheduleId) || !int.TryParse(model.TrainingId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _) || string.IsNullOrEmpty(model.TrainingType) || string.IsNullOrEmpty(model.DayOfWeek) || !DateTime.TryParse(model.StartTime.ToString(), out _) || !DateTime.TryParse(model.EndTime.ToString(), out _))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Schedule.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var schedule = await _repositoryWrapper.Schedule
                .FindByCondition(x => x.ScheduleId == id);

            await _repositoryWrapper.Schedule.Delete(schedule.First());
            await _repositoryWrapper.Save();
        }
    }
}
