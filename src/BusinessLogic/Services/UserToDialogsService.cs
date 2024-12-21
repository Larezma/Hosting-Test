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
    public class UserToDialogsService : IUserToDialogsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserToDialogsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserToDialog>> GetAll()
        {
            return await _repositoryWrapper.UserToDialog.FindALL();
        }

        public async Task<UserToDialog> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var userToDialogs = await _repositoryWrapper.UserToDialog
                .FindByCondition(x => x.Id == id);
            return userToDialogs.First();
        }

        public async Task Create(UserToDialog model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.DialogId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) ||  int.IsNegative(model.Id) || int.IsNegative(model.DialogId) || int.IsNegative(model.UserId) || model.DialogId <= 0 || model.UserId <= 0)
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.UserToDialog.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(UserToDialog model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.Id) || !int.TryParse(model.DialogId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) || int.IsNegative(model.Id) || int.IsNegative(model.DialogId) || int.IsNegative(model.UserId) || model.Id <= 0 || model.DialogId <= 0 || model.UserId <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.UserToDialog.Update(model);
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
            var userToDialogs = await _repositoryWrapper.UserToDialog
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.UserToDialog.Delete(userToDialogs.First());
            await _repositoryWrapper.Save();
        }
    }
}
