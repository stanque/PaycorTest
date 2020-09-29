using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaycorTest.Data
{
    public interface IRepositoryBase<T> where T: class
    {
        IQueryable<T> All(bool disableTracking = false);

        IQueryable<T> All(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool disableTracking = true);

        IQueryable<T> All
        (
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
        );

        IQueryable<T> All
        (
            IEnumerable<Expression<Func<T, bool>>> predicates,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
        );

        DbSet<T> AllAsDbSet();

        T Add(T entity);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);
    }

    public interface IRepository<TKey, T> : IRepositoryBase<T> where T: class
    {
        T Find(TKey id);

        Task<T> FindAsync(TKey id, bool disableTracking = true);

        void DeleteById(TKey id);

        Task DeleteByIdAsync(TKey id);
    }

    public interface IRepository<TKey1, TKey2, T> : IRepositoryBase<T> where T : class
    {
        T Find(TKey1 id1, TKey2 id2);

        Task<T> FindAsync(TKey1 id1, TKey2 id2, bool disableTracking = true);

        void DeleteById(TKey1 id1, TKey2 id2);

        Task DeleteByIdAsync(TKey1 id1, TKey2 id2);
    }

    public interface IRepository<TKey1, TKey2, TKey3, T> : IRepositoryBase<T> where T : class
    {
        T Find(TKey1 id1, TKey2 id2, TKey3 id3);

        Task<T> FindAsync(TKey1 id1, TKey2 id2, TKey3 id3, bool disableTracking = true);

        void DeleteById(TKey1 id1, TKey2 id2, TKey3 id3);

        Task DeleteByIdAsync(TKey1 id1, TKey2 id2, TKey3 id3);
    }
}
