using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IGroupMembersService
    {
        Task<List<GroupMember>> GetAll();
        Task<GroupMember> GetById(int id);
        Task Create(GroupMember model);
        Task Update(GroupMember model);
        Task Delete(int id);
    }
}