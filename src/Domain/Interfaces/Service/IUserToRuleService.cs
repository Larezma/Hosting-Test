using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUserToRuleService
    {
        Task<List<UserToRule>> GetAll();
        Task<UserToRule> GetById(int id);
        Task Create(UserToRule model);
        Task Update(UserToRule model);
        Task Delete(int id);
    }
}
