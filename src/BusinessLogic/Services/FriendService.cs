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
    public class FriendService : IFriendService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FriendService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Friend>> GetAll()
        {
            return await _repositoryWrapper.Friend.FindALL();
        }

        public async Task<Friend> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var friend = await _repositoryWrapper.Friend
                .FindByCondition(x => x.FriendId == id);
            return friend.First();
        }

        public async Task Create(Friend model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.UserId1.ToString(), out _) || !int.TryParse(model.UserId2.ToString(), out _) || int.IsNegative(model.UserId1) || int.IsNegative(model.UserId2) || model.UserId1 <= 0 || model.UserId2 <= 0)
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Friend.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Friend model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.FriendId) || !int.TryParse(model.UserId1.ToString(), out _) || !int.TryParse(model.UserId2.ToString(), out _) || int.IsNegative(model.UserId1) || int.IsNegative(model.UserId2) || model.UserId1 <= 0 || model.UserId2 <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Friend.Update(model);
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
            var friend = await _repositoryWrapper.Friend
                .FindByCondition(x => x.FriendId == id);

            await _repositoryWrapper.Friend.Delete(friend.First());
            await _repositoryWrapper.Save();
        }
    }
}
