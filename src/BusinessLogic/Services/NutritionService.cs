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
    public class NutritionService : INutritionService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public NutritionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Nutrition>> GetAll()
        {
            return await _repositoryWrapper.Nutrition.FindALL();
        }

        public async Task<Nutrition> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");

            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var users = await _repositoryWrapper.Nutrition
            .FindByCondition(x => x.NutritionId == id);
            return users.First();
        }

        public async Task Create(Nutrition model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.Product.ToString(), out _) || string.IsNullOrEmpty(model.MeanType) || string.IsNullOrEmpty(model.MeanDeacription) || !DateTime.TryParse(model.DateNutrition.ToString(), out _))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Nutrition.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Nutrition model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.NutritionId) || int.IsNegative(model.Product) || !int.TryParse(model.Product.ToString(), out _) || string.IsNullOrEmpty(model.MeanType) || string.IsNullOrEmpty(model.MeanDeacription))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Nutrition.Update(model);
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
            var nutrition = await _repositoryWrapper.Nutrition
                .FindByCondition(x => x.NutritionId == id);

            await _repositoryWrapper.Nutrition.Delete(nutrition.First());
            await _repositoryWrapper.Save();
        }
    }
}