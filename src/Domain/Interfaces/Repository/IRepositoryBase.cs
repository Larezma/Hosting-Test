using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> FindALL();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
