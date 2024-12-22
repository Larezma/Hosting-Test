using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Domain.Interfaces.Service
{
    public interface IRolesService
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task Create(Role model);
        Task Update(Role model);
        Task Delete(int id);
    }
}
