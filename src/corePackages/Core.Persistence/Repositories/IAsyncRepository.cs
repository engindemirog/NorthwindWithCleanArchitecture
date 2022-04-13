using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{

    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
                             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                             int index = 0,
                             int size = 10,
                             bool enableTracking = true,
                             CancellationToken cancellationToken = default);

        IQueryable<T> Query();

        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
