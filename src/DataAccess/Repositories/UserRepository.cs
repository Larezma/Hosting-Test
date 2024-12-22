using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(VitalityMasteryTestContext masteryContext) :base(masteryContext) 
        {
        }
    }
}
