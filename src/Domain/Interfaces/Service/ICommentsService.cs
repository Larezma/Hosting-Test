using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Interfaces.Service
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetAll();
        Task<Comment> GetById(int id);
        Task Create(Comment model);
        Task Update(Comment model);
        Task Delete(int id);
    }
}