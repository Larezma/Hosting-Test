using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IDialogsService
    {
        Task<List<Dialog>> GetAll();
        Task<Dialog> GetById(int id);
        Task Create(Dialog model);
        Task Update(Dialog model);
        Task Delete(int id);
    }
}
