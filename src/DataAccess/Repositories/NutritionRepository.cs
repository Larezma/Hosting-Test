using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class NutritionRepository : RepositoryBase<Nutrition>, INutritionRepository
    {
        public NutritionRepository(VitalityMasteryTestContext masteryContext) : base(masteryContext)
        {
        }
    }
}