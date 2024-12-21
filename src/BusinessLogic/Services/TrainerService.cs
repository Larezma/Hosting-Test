using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TrainerService : ITrainerService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TrainerService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Trainer>> GetAll()
        {
            return await _repositoryWrapper.Trainer.FindALL();
        }

        public async Task<Trainer> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var trainer = await _repositoryWrapper.Trainer
                .FindByCondition(x => x.TrainerId == id);
            return trainer.First();
        }

        public async Task Create(Trainer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.MiddleName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.PhoneNumber))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.Trainer.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Trainer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.TrainerId) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.MiddleName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.PhoneNumber))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Trainer.Update(model);
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
            var trainer = await _repositoryWrapper.Trainer
                .FindByCondition(x => x.TrainerId == id);

            await _repositoryWrapper.Trainer.Delete(trainer.First());
            await _repositoryWrapper.Save();
        }
    }
}