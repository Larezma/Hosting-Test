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
    public class AchievementsService : IAchievementsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AchievementsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Achievement>> GetAll()
        {
            return await _repositoryWrapper.Achievements.FindALL();
        }

        public async Task<Achievement> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var achievements = await _repositoryWrapper.Achievements
                .FindByCondition(x => x.AchievementsId == id);
            return achievements.First();
        }

        public async Task Create(Achievement model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.AchievementsText) || int.IsNegative(model.AchievementsType) || string.IsNullOrEmpty(model.AchievementsText) || int.IsNegative(model.AchievementsType))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.Achievements.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Achievement model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.AchievementsId) || string.IsNullOrEmpty(model.AchievementsText) || int.IsNegative(model.AchievementsType) || string.IsNullOrEmpty(model.AchievementsText) || int.IsNegative(model.AchievementsType))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Achievements.Update(model);
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
            var achievements = await _repositoryWrapper.Achievements
                .FindByCondition(x => x.AchievementsId == id);

            await _repositoryWrapper.Achievements.Delete(achievements.First());
            await _repositoryWrapper.Save();
        }
    }
}