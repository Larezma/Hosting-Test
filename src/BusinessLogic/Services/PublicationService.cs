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
    public class PublicationService : IPublicationService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PublicationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Publication>> GetAll()
        {
            return await _repositoryWrapper.Publication.FindALL();
        }

        public async Task<Publication> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var publications = await _repositoryWrapper.Publication
                .FindByCondition(x => x.PublicationsId == id);
            return publications.First();
        }

        public async Task Create(Publication model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!int.TryParse(model.UsersId.ToString(), out _) || string.IsNullOrEmpty(model.PublicationText))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }
            await _repositoryWrapper.Publication.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Publication model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.PublicationsId) || int.IsNegative(model.PublicationsId) || !int.TryParse(model.UsersId.ToString(), out _) || string.IsNullOrEmpty(model.PublicationText))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Publication.Update(model);
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
            var publications = await _repositoryWrapper.Publication
                .FindByCondition(x => x.PublicationsId == id);

            await _repositoryWrapper.Publication.Delete(publications.First());
            await _repositoryWrapper.Save();
        }
    }
}