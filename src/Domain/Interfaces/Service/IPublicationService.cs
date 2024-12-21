using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IPublicationService
    {
        Task<List<Publication>> GetAll();
        Task<Publication> GetById(int id);
        Task Create(Publication model);
        Task Update(Publication model);
        Task Delete(int id);
    }
}
