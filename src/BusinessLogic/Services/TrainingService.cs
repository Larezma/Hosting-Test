using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TrainingService : ITrainingService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TrainingService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Training>> GetAll()
        {
            return await _repositoryWrapper.Training.FindALL();
        }

        public async Task<Training> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var training = await _repositoryWrapper.Training
                .FindByCondition(x => x.TrainingId == id);
            return training.First();
        }

        public async Task Create(Training model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.DurationMinutes) || !decimal.TryParse(model.CaloriesBurned.ToString(), out _) || string.IsNullOrEmpty(model.Notes) || string.IsNullOrEmpty(model.TrainingType))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Training.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Training model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.TrainingId) || string.IsNullOrEmpty(model.DurationMinutes) || !decimal.TryParse(model.CaloriesBurned.ToString(), out _) || string.IsNullOrEmpty(model.TrainingType))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Training.Update(model);
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
            var training = await _repositoryWrapper.Training
                .FindByCondition(x => x.TrainingId == id);

            await _repositoryWrapper.Training.Delete(training.First());
            await _repositoryWrapper.Save();
        }
    }
}