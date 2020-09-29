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
    public class Repository<TKey,T> : BaseRepository<T>, IRepository<TKey,T> where T : class
    {
        public Repository(AdvantureWorksContext dbContext)
            :base(dbContext)
        {
        }

        public void DeleteById(TKey id)
        {
            var entity = Find(id);

            if (entity == null)
            {
                throw new ArgumentException($"The entity of type {typeof(T).FullName} with an id {id} was not found!");
            }

            Delete(entity);
        }

        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"The entity of type {typeof(T).FullName} with an id {id} was not found!");
            }

            await DeleteAsync(entity);
        }

        public T Find(TKey id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> FindAsync(TKey id, bool disableTracking = true)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (disableTracking && entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
    }
}
