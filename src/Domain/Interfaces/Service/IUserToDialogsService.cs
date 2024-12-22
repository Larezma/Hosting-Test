using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUserToDialogsService
    {
        Task<List<UserToDialog>> GetAll();
        Task<UserToDialog> GetById(int id);
        Task Create(UserToDialog model);
        Task Update(UserToDialog model);
        Task Delete(int id);
    }
}
