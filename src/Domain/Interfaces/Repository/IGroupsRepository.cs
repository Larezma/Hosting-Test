using Domain.Interfaces.Wrapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Domain.Models.Group;

namespace Domain.Interfaces.Repository
{
    public interface IGroupsRepository : IRepositoryBase<Group>
    {
    }
}