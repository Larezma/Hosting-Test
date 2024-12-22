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
    public class UserToAchievementsService : IUserToAchievementService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserToAchievementsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserToAchievement>> GetAll()
        {
            return await _repositoryWrapper.UserToAchievements.FindALL();
        }

        public async Task<UserToAchievement> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var userToAchievements = await _repositoryWrapper.UserToAchievements
                .FindByCondition(x => x.Id == id);
            return userToAchievements.First();
        }

        public async Task Create(UserToAchievement model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.AchievementsId.ToString(), out _) || int.IsNegative(model.UserId) || int.IsNegative(model.AchievementsId) || model.UserId <= 0 || model.AchievementsId <= 0)
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.UserToAchievements.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(UserToAchievement model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.UserId) || !int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.AchievementsId.ToString(), out _) || int.IsNegative(model.UserId) || int.IsNegative(model.AchievementsId) || model.Id <= 0 || model.UserId <= 0 || model.AchievementsId <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.UserToAchievements.Update(model);
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
            var userToAchievements = await _repositoryWrapper.UserToAchievements
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.UserToAchievements.Delete(userToAchievements.First());
            await _repositoryWrapper.Save();
        }
    }
}
