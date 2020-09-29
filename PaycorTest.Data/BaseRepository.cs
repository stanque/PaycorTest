using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PaycorTest.Data
{
    public abstract class BaseRepository<T> : IRepositoryBase<T> where T: class
    {
        protected readonly AdvantureWorksContext _dbContext;

        public BaseRepository(AdvantureWorksContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            var entry = _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entry.Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> All(bool disableTracking = false)
        {
            if (disableTracking)
            {
                return _dbContext.Set<T>().AsNoTracking().AsQueryable();
            }

            return _dbContext.Set<T>().AsQueryable();
        }

        public DbSet<T> AllAsDbSet()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> All(params Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public IQueryable<T> AllNoTracking(params Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            return query.AsNoTracking();
        }

        public IQueryable<T> All(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    MemberExpression memberExpression = (MemberExpression)include.Body;

                    var memberExpressionString = memberExpression.ToString();
                    string includePart = null;
                    List<string> thenIncludeParts = new List<string>();

                    //Down to nTh level of nested properties include support
                    if (memberExpressionString.Count(x => x == '.') >= 2)
                    {
                        var expressionParts = memberExpressionString.Split(new char[] { '.' });
                        includePart = expressionParts[1];
                        for (int i = 2; i < expressionParts.Length; ++i)
                        {
                            thenIncludeParts.Add(expressionParts[i]);
                        }
                    }

                    if (string.IsNullOrEmpty(includePart))
                    {
                        query = query.Include(memberExpression.Member.Name);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(includePart);
                        foreach (var part in thenIncludeParts)
                        {
                            sb.Append($".{part}");
                        }

                        query = query.Include(sb.ToString());
                    }
                }
            }
            return query.AsQueryable();
        }

        public IQueryable<T> AllNoTracking(params Expression<Func<T, object>>[] includes)
        {
            return All(includes).AsNoTracking().AsQueryable();
        }

        public IQueryable<T> All(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool disableTracking = true)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (include != null)
            {
                query = include(query);
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public IQueryable<T> All
        (
            IEnumerable<Expression<Func<T, bool>>> predicates,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
        )
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            return query.AsQueryable();
        }

        public IQueryable<T> All
        (
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
        )
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.AsQueryable();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
