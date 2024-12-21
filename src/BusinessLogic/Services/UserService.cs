using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;
using System.Reflection;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindALL();
        }

        public async Task<User> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
              
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var users = await _repositoryWrapper.User
            .FindByCondition(x => x.UserId == id);
            return users.First();
        }

        public async Task Create(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.PhoneNumber))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.User.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.UserId) || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.PhoneNumber))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.User.Update(model);
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
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);

            await _repositoryWrapper.User.Delete(user.First());
            await _repositoryWrapper.Save();
        }
    }
}
