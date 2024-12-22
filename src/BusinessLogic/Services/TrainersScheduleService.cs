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
    public class TrainersScheduleService : ITrainersScheduleService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TrainersScheduleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<TrainersSchedule>> GetAll()
        {
            return await _repositoryWrapper.TrainersSchedule.FindALL();
        }

        public async Task<TrainersSchedule> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var trainersSchedule = await _repositoryWrapper.TrainersSchedule
                .FindByCondition(x => x.Id == id);
            return trainersSchedule.First();
        }

        public async Task Create(TrainersSchedule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.ScheduleId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _) || string.IsNullOrEmpty(model.TypeOfTraining) 
                || !DateTime.TryParse(model.Date.ToString(), out _) || !DateTime.TryParse(model.Time.ToString(), out _) || int.IsNegative(model.ScheduleId) || int.IsNegative(model.TrainerId))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.TrainersSchedule.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(TrainersSchedule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.Id) || !int.TryParse(model.ScheduleId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _) || string.IsNullOrEmpty(model.TypeOfTraining)
                || !DateTime.TryParse(model.Date.ToString(), out _) || !DateTime.TryParse(model.Time.ToString(), out _) || int.IsNegative(model.ScheduleId) || int.IsNegative(model.TrainerId))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.TrainersSchedule.Update(model);
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
            var trainersSchedule = await _repositoryWrapper.TrainersSchedule
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.TrainersSchedule.Delete(trainersSchedule.First());
            await _repositoryWrapper.Save();
        }
    }
}
