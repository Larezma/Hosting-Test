using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IMessageUsersService
    {
        Task<List<MessageUser>> GetAll();
        Task<MessageUser> GetById(int id);
        Task Create(MessageUser model);
        Task Update(MessageUser model);
        Task Delete(int id);
    }
}
