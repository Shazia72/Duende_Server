using CMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Implemenation
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            dbSet = _appDbContext.Set<T>();
        }

        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (_appDbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_appDbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return entity;
        }

        public void DeleteRange(List<T> entityList)
        {
           dbSet.RemoveRange(entityList);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy!= null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T GetByIdSync(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, 
            bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (filter != null)
            {
                query = include(query);
            }
            return query.FirstOrDefault();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
