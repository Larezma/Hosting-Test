using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces.Wrapper;
using Group = Domain.Models.Group;

namespace Domain.Interfaces.Repository
{
    public interface IGroupsRepository : IRepositoryBase<Group>
    {
    }
}
