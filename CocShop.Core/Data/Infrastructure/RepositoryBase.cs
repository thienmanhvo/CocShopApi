using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CocShop.Core.Data.Query;
using CocShop.Core.Extentions;
using System.Security.Claims;

namespace CocShop.Core.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private DataContext dataContext;
        private readonly DbSet<T> dbSet;
        private IServiceProvider _serviceProvider;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DataContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory, IServiceProvider serviceProvider)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
            _serviceProvider = serviceProvider;
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Add(ICollection<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {

            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetById(String id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? offset = null, int? limit = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;//.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (offset != null && limit != null) //&& (offset >= 0 && limit > 0))
            {
                return query.Skip(offset.Value).Take(limit.Value);
            }
            else
            {
                return query;
            }

        }
        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string sortBy = null, int? offset = null, int? limit = null, IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = query.OrderBy(sortBy);

            if (offset != null && limit != null) //&& (offset >= 0 && limit > 0))
            {
                return query.Skip(offset.Value).Take(limit.Value);
            }
            else
            {
                return query;
            }
        }

        public string GetUsername()
        {
            try
            {
                var accessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User?.FindFirst(Constants.CLAIM_USERNAME)?.Value ?? Constants.USER_ANONYMOUS;
            }
            catch
            {
                return "SYSTEM";
            }
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Count();
        }
        public string GetCurrentUserId()
        {
            var accessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        #endregion
    }
}
