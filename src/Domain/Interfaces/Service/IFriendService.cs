using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IFriendService
    {
        Task<List<Friend>> GetAll();
        Task<Friend> GetById(int id);
        Task Create(Friend model);
        Task Update(Friend model);
        Task Delete(int id);
    }
}