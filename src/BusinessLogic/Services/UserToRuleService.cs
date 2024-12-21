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
    public class UserToRuleService : IUserToRuleService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserToRuleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserToRule>> GetAll()
        {
            return await _repositoryWrapper.UserToRule.FindALL();
        }

        public async Task<UserToRule> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var userToRules = await _repositoryWrapper.UserToRule
                .FindByCondition(x => x.Id == id);
            return userToRules.First();
        }

        public async Task Create(UserToRule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.Id) || int.IsNegative(model.UserId) || int.IsNegative(model.RoleId) || int.IsNegative(model.Id) || int.IsNegative(model.UserId) || int.IsNegative(model.RoleId) || model.UserId <= 0 || model.RoleId <= 0)
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.UserToRule.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(UserToRule model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.Id) || int.IsNegative(model.Id) || int.IsNegative(model.UserId) || int.IsNegative(model.RoleId) || int.IsNegative(model.Id) || int.IsNegative(model.UserId) || int.IsNegative(model.RoleId) || model.Id <= 0 || model.UserId <= 0 || model.RoleId <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.UserToRule.Update(model);
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
            var userToRules = await _repositoryWrapper.UserToRule
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.UserToRule.Delete(userToRules.First());
            await _repositoryWrapper.Save();
        }
    }
}