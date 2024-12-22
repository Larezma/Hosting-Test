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
    public class UserTrainingService : IUserTrainingService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserTrainingService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserTraining>> GetAll()
        {
            return await _repositoryWrapper.UserTraining.FindALL();
        }

        public async Task<UserTraining> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var userTraining = await _repositoryWrapper.UserTraining
                .FindByCondition(x => x.Id == id);
            return userTraining.First();
        }

        public async Task Create(UserTraining model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.TrainingId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _) 
                || string.IsNullOrEmpty(model.DayOfWeek) || string.IsNullOrEmpty(model.Duration)  || int.IsNegative(model.TrainingId) || int.IsNegative(model.UserId) || int.IsNegative(model.TrainerId))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.UserTraining.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(UserTraining model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.Id) || !int.TryParse(model.TrainingId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.TrainerId.ToString(), out _)
                || string.IsNullOrEmpty(model.DayOfWeek) || string.IsNullOrEmpty(model.Duration) || int.IsNegative(model.TrainingId) || int.IsNegative(model.UserId) || int.IsNegative(model.TrainerId))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.UserTraining.Update(model);
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
            var userTraining = await _repositoryWrapper.UserTraining
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.UserTraining.Delete(userTraining.First());
            await _repositoryWrapper.Save();
        }
    }
}
