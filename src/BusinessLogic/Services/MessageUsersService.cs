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
    public class MessageUsersService : IMessageUsersService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public MessageUsersService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<MessageUser>> GetAll()
        {
            return await _repositoryWrapper.MessageUsers.FindALL();
        }

        public async Task<MessageUser> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var messageUsers = await _repositoryWrapper.MessageUsers
                .FindByCondition(x => x.MessageId == id);
            return messageUsers.First();
        }

        public async Task Create(MessageUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.SenderId.ToString(), out _) || !int.TryParse(model.ReceiverId.ToString(), out _) || string.IsNullOrEmpty(model.MessageContent))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.MessageUsers.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(MessageUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.MessageId) || int.IsNegative(model.SenderId) || int.IsNegative(model.ReceiverId) || !int.TryParse(model.SenderId.ToString(), out _) || !int.TryParse(model.ReceiverId.ToString(), out _) || string.IsNullOrEmpty(model.MessageContent))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.MessageUsers.Update(model);
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
            var messageUsers = await _repositoryWrapper.MessageUsers
                .FindByCondition(x => x.MessageId == id);

            await _repositoryWrapper.MessageUsers.Delete(messageUsers.First());
            await _repositoryWrapper.Save();
        }
    }
}
