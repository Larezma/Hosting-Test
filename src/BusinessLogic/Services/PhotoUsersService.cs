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
    public class PhotoUsersService : IPhotoUsersService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PhotoUsersService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<PhotoUser>> GetAll()
        {
            return await _repositoryWrapper.PhotoUsers.FindALL();
        }

        public async Task<PhotoUser> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");

            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var users = await _repositoryWrapper.PhotoUsers
            .FindByCondition(x => x.PhotoId == id);
            return users.First();
        }

        public async Task Create(PhotoUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!decimal.TryParse(model.UserId.ToString(), out _) || string.IsNullOrEmpty(model.PhotoLink))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.PhotoUsers.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(PhotoUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.PhotoId) || !decimal.TryParse(model.UserId.ToString(), out _) || string.IsNullOrEmpty(model.PhotoLink))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.PhotoUsers.Update(model);
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
            var photoUsers = await _repositoryWrapper.PhotoUsers
                .FindByCondition(x => x.PhotoId == id);

            await _repositoryWrapper.PhotoUsers.Delete(photoUsers.First());
            await _repositoryWrapper.Save();
        }
    }
}