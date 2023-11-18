using System.Linq.Expressions;
using System;

namespace MyVaccine.WebApi.Repositories.Contracts
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        Task AddRange(List<T> entities);

        Task Update(T entity);
        Task UpdateRange(List<T> entities);
        Task Delete(T entity);
        Task DeleteRange(List<T> entities);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task Patch(T entity);
        Task PatchRange(List<T> entity);
        IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate);


    }
}

