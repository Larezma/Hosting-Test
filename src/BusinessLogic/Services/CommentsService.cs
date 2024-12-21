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
    public class CommentsService : ICommentsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CommentsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _repositoryWrapper.Comments.FindALL();
        }

        public async Task<Comment> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var comments = await _repositoryWrapper.Comments
                .FindByCondition(x => x.CommentsId == id);
            return comments.First();
        }

        public async Task Create(Comment model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.ItemId.ToString(), out _) || !int.TryParse(model.ItemType.ToString(), out _) || string.IsNullOrEmpty(model.CommentsText) || int.IsNegative(model.UserId) || int.IsNegative(model.ItemId) || int.IsNegative(model.ItemType))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.Comments.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Comment model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.CommentsId) || !int.TryParse(model.UserId.ToString(), out _) || !int.TryParse(model.ItemId.ToString(), out _) || !int.TryParse(model.ItemType.ToString(), out _) || string.IsNullOrEmpty(model.CommentsText) || int.IsNegative(model.UserId) || int.IsNegative(model.ItemId) || int.IsNegative(model.ItemType))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Comments.Update(model);
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
            var comments = await _repositoryWrapper.Comments
                .FindByCondition(x => x.CommentsId == id);

            await _repositoryWrapper.Comments.Delete(comments.First());
            await _repositoryWrapper.Save();
        }
    }
}
