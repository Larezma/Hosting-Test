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
    public class UserNutritionService : IUserNutritionService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserNutritionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserNutrition>> GetAll()
        {
            return await _repositoryWrapper.UserNutrition.FindALL();
        }

        public async Task<UserNutrition> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var userNutritions = await _repositoryWrapper.UserNutrition
                .FindByCondition(x => x.UserNutritionId == id);
            return userNutritions.First();
        }

        public async Task Create(UserNutrition model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.NutritionId.ToString(), out _) || !DateTime.TryParse(model.DateOfAdmission.ToString(), out _)
                || !DateTime.TryParse(model.AppointmentTime.ToString(), out _) || string.IsNullOrEmpty(model.NutritionType) || string.IsNullOrEmpty(model.Report)
                || int.IsNegative(model.UserId) || (int.IsNegative(model.NutritionId)))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.UserNutrition.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(UserNutrition model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.UserId) || !int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.NutritionId.ToString(), out _) || !DateTime.TryParse(model.DateOfAdmission.ToString(), out _)
                || !DateTime.TryParse(model.AppointmentTime.ToString(), out _) || string.IsNullOrEmpty(model.NutritionType) || string.IsNullOrEmpty(model.Report)
                || int.IsNegative(model.UserId) || int.IsNegative(model.NutritionId))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.UserNutrition.Update(model);
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
            var userNutritions = await _repositoryWrapper.UserNutrition
                .FindByCondition(x => x.UserNutritionId == id);

            await _repositoryWrapper.UserNutrition.Delete(userNutritions.First());
            await _repositoryWrapper.Save();
        }
    }
}