using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        public FriendRepository(VitalityMasteryTestContext masteryContext) : base(masteryContext)
        {
        }
    }
}