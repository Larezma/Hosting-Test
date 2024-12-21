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
    public class GroupsService : IGroupsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GroupsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Group>> GetAll()
        {
            return await _repositoryWrapper.Group.FindALL();
        }

        public async Task<Group> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var group = await _repositoryWrapper.Group
                .FindByCondition(x => x.GroupsId == id);
            return group.First();
        }

        public async Task Create(Group model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.OwnerGroups.ToString(), out _) || string.IsNullOrEmpty(model.GroupsName))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Group.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Group model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.GroupsId) || int.IsNegative(model.OwnerGroups) || !int.TryParse(model.OwnerGroups.ToString(), out _) || string.IsNullOrEmpty(model.GroupsName))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Group.Update(model);
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
            var group = await _repositoryWrapper.Group
                .FindByCondition(x => x.GroupsId == id);

            await _repositoryWrapper.Group.Delete(group.First());
            await _repositoryWrapper.Save();
        }
    }
}
