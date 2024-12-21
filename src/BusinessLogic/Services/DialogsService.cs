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
    public class DialogsService : IDialogsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DialogsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Dialog>> GetAll()
        {
            return await _repositoryWrapper.Dialogs.FindALL();
        }

        public async Task<Dialog> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var dialogs = await _repositoryWrapper.Dialogs
                .FindByCondition(x => x.DialogsId == id);
            return dialogs.First();
        }

        public async Task Create(Dialog model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.DialogsId.ToString(), out _) || int.IsNegative(model.DialogsId) || string.IsNullOrEmpty(model.TextDialogs) || string.IsNullOrEmpty(model.TextDialogs))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Dialogs.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Dialog model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.DialogsId) || !int.TryParse(model.DialogsId.ToString(), out _) || int.IsNegative(model.DialogsId) || string.IsNullOrEmpty(model.TextDialogs) || string.IsNullOrEmpty(model.TextDialogs))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Dialogs.Update(model);
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
            var dialogs = await _repositoryWrapper.Dialogs
                .FindByCondition(x => x.DialogsId == id);

            await _repositoryWrapper.Dialogs.Delete(dialogs.First());
            await _repositoryWrapper.Save();
        }
    }
}