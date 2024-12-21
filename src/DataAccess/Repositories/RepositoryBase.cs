using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected VitalityMasteryTestContext MasteryContext { get; set; }
        public RepositoryBase(VitalityMasteryTestContext masteryContext)
        {
            MasteryContext = masteryContext;
        }

        public async Task<List<T>> FindALL() => await MasteryContext.Set<T>().AsNoTracking().ToListAsync();
        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression) =>
            await MasteryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        public async Task Create(T entity) => await MasteryContext.Set<T>().AddAsync(entity);
        public async Task Update(T entity) => MasteryContext.Set<T>().Update(entity);
        public async Task Delete(T entity) => MasteryContext.Set<T>().Remove(entity);
    }
}