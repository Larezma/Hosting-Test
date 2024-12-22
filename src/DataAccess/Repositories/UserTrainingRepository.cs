using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class UserTrainingRepository : RepositoryBase<UserTraining> , IUserTrainingRepository
    {
        public UserTrainingRepository(VitalityMasteryTestContext masteryContext) : base(masteryContext)
        {
        }
    }
}
