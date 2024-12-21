using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Domain.Models.Group;

namespace Domain.Interfaces.Service
{
    public interface IGroupsService
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int id);
        Task Create(Group model);
        Task Update(Group model);
        Task Delete(int id);
    }
}