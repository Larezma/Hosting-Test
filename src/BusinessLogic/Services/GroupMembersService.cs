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
    public class GroupMembersService : IGroupMembersService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GroupMembersService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<GroupMember>> GetAll()
        {
            return await _repositoryWrapper.GroupMembers.FindALL();
        }

        public async Task<GroupMember> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var groupMembers = await _repositoryWrapper.GroupMembers
                .FindByCondition(x => x.GroupsId == id);
            return groupMembers.First();
        }

        public async Task Create(GroupMember model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.GroupsId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) || int.IsNegative(model.GroupsId) || int.IsNegative(model.UserId) || model.UserId <= 0 || model.GroupsId <= 0)
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.GroupMembers.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(GroupMember model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.GroupsId.ToString(), out _) || !int.TryParse(model.UserId.ToString(), out _) || int.IsNegative(model.GroupsId) || int.IsNegative(model.UserId) || model.UserId <= 0 || model.GroupsId <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.GroupMembers.Update(model);
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
            var groupMembers = await _repositoryWrapper.GroupMembers
                .FindByCondition(x => x.GroupsId == id);

            await _repositoryWrapper.GroupMembers.Delete(groupMembers.First());
            await _repositoryWrapper.Save();
        }
    }
}
