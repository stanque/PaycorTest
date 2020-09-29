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
    public class Repository<TKey1,TKey2,T> 
        : BaseRepository<T>, IRepository<TKey1, TKey2, T> where T : class
    {
        public Repository(AdvantureWorksContext context) : base(context)
        {
        }

        public void DeleteById(TKey1 id1, TKey2 id2)
        {
            var entity = Find(id1, id2);

            if (entity == null)
            {
                throw new ArgumentException($"The entity of type {typeof(T).FullName} with an id combination {id1}, {id2} was not found!");
            }

            Delete(entity);
        }

        public async Task DeleteByIdAsync(TKey1 id1, TKey2 id2)
        {
            var entity = await FindAsync(id1, id2);

            if (entity == null)
            {
                throw new ArgumentException($"The entity of type {typeof(T).FullName} with an id combination {id1}, {id2} was not found!");
            }

            await DeleteAsync(entity);
        }

        public T Find(TKey1 id1, TKey2 id2)
        {
            return _dbContext.Set<T>().Find(id1, id2);
        }

        public async Task<T> FindAsync(TKey1 id1, TKey2 id2, bool disableTracking = true)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id1, id2);
            if (disableTracking && entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
    }
}
