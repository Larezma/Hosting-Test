using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Service;

namespace DataAccess.Repositories
{
    internal class TrainersScheduleRepository : RepositoryBase<TrainersSchedule> , ITrainersScheduleRepository
    {
        public TrainersScheduleRepository(VitalityMasteryTestContext masteryContext) : base(masteryContext)
        {
        }
    }
}
