//using AutoMapper.QueryableExtensions;
//using GICBC.Common.Interfaces;
//using GICBC.Common.Abstracts;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace GICBC.Common.Abstracts
//{
//    public class Repository<T> : IRepository<T>
//            where T : class, IEntity, new()
//    {
//        protected readonly DbContext _context;
//        protected readonly IActionContextAccessor _ctxAccessor;
//        private readonly bool _forceAllItems;
//        #region Properties
//        public Repository(DbContext context, IActionContextAccessor ctxAccessor, bool forceAllItems)
//        {
//            _context = context;
//            _ctxAccessor = ctxAccessor;
//            _forceAllItems = forceAllItems;
//        }
//        #endregion

//        #region Query without project
//        public IEnumerable<T> GetAll(string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }

//            return query.OrderBy(order).AsEnumerable();
//        }


//        public IEnumerable<T> GetAll(string order, int pageIndex, int limit, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).AsEnumerable();
//        }


//        public async Task<IEnumerable<T>> GetAllAsync(string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.OrderBy(order).ToListAsync();
//        }

//        public async Task<IEnumerable<T>> GetAllAsync(string order, int pageIndex, int limit, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ToListAsync();
//        }

//        public async Task<IEnumerable<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> order, int pageIndex, int limit, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ToListAsync();
//        }

//        public T FindById(Guid id, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.FirstOrDefault(x => x.Id == id);
//        }

//        public T FindById(Guid id)
//        {
//            IQueryable<T> query = Query;
//            return query.FirstOrDefault(x => x.Id == id);
//        }
//        public async Task<T> FindByIdAsync(Guid id)
//        {
//            IQueryable<T> query = Query;
//            return await query.FirstOrDefaultAsync(x => x.Id == id);
//        }


//        public async Task<T> FindByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.FirstOrDefaultAsync(e => e.Id == id);
//        }
//        public T GetSingle(Expression<Func<T, bool>> predicate, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            query = query.OrderBy(order);
//            if (predicate == null)
//            {
//                return query.FirstOrDefault();
//            }
//            return query.Where(predicate).FirstOrDefault();
//        }

//        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            query = query.OrderBy(order);
//            if (predicate == null)
//            {
//                return await query.FirstOrDefaultAsync();
//            }
//            return await query.Where(predicate).FirstOrDefaultAsync();
//        }
//        public async Task<T> GetSingleAsync(string predicate, object[] parameters, string order = "")
//        {
//            return await Query.Where(predicate, parameters).OrderBy(order).FirstOrDefaultAsync();
//        }

//        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.Where(predicate).OrderBy(order).AsEnumerable();
//        }
//        public IEnumerable<T> FindBy(string order, int pageIndex, int limit, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.Where(predicate).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).AsEnumerable();
//        }
//        public IEnumerable<T> FindBy(string predicate, object[] parameters, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.Where<T>(predicate, parameters).OrderBy(order).AsEnumerable();
//        }
//        public IEnumerable<T> FindBy(string order, int pageIndex, int limit, string predicate, object[] parameters, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query.Where(predicate, parameters).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).AsEnumerable();
//        }
       
//        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.Where(predicate).OrderBy(order).ToListAsync();
//        }
//        public async Task<IEnumerable<T>> FindByAsync(string order, int pageIndex, int limit, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.Where(predicate).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ToListAsync();
//        }
//        public async Task<IEnumerable<T>> FindByAsync(string predicate, object[] parameters, string order = "", params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.Where(predicate, parameters).OrderBy(order).ToListAsync();
//        }
//        public async Task<IEnumerable<T>> FindByAsync(string order, int pageIndex, int limit, string predicate, object[] parameters, params Expression<Func<T, object>>[] includeProperties)
//        {
//            IQueryable<T> query = Query;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return await query.Where(predicate, parameters).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ToListAsync();
//        }

//        #endregion
//        #region query with project
//        public IEnumerable<H> GetAll<H>(string order = "")
//        {
//            return Query.OrderBy(order).ProjectTo<H>().AsEnumerable();
//        }
//        public IEnumerable<H> GetAll<H>(string order, int pageIndex, int limit)
//        {
//            return Query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().AsEnumerable();
//        }
//        public async Task<IEnumerable<H>> GetAllAsync<H>(string order = "")
//        {
//            return await Query.OrderBy(order).ProjectTo<H>().ToListAsync();
//        }
//        public async Task<IEnumerable<H>> GetAllAsync<H>(string order, int pageIndex, int limit)
//        {
//            return await Query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().ToListAsync();
//        }
//        public async Task<IEnumerable<H>> GetAllAsync<H, TKey>(Expression<Func<T, TKey>> order, int pageIndex, int limit)
//        {
//            return await Query.OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().ToListAsync();
//        }
//        public H FindById<H>(Guid id)
//        {
//            return Query.Where(x => x.Id == id).ProjectTo<H>().FirstOrDefault();
//        }
//        public async Task<H> FindByIdAsync<H>(Guid id)
//        {

//            return await Query.Where(x => x.Id == id).ProjectTo<H>().FirstOrDefaultAsync();
//        }
//        public H GetSingle<H>(Expression<Func<T, bool>> predicate, string order = "")
//        {
//            if (predicate != null)
//            {
//                return Query.OrderBy(order).ProjectTo<H>().FirstOrDefault();
//            }
//            return Query.Where(predicate).OrderBy(order).ProjectTo<H>().FirstOrDefault();
//        }
//        public async Task<H> GetSingleAsync<H>(Expression<Func<T, bool>> predicate, string order = "")
//        {
//            IQueryable<T> query = Query;
//            return await Query.Where(predicate).OrderBy(order).ProjectTo<H>().FirstOrDefaultAsync();
//        }

//        public IEnumerable<H> FindBy<H>(Expression<Func<T, bool>> predicate, string order = "")
//        {
//            return Query.Where(predicate).OrderBy(order).ProjectTo<H>().AsEnumerable();
//        }
//        public IEnumerable<H> FindBy<H>(string order, int pageIndex, int limit, Expression<Func<T, bool>> predicate)
//        {
//            return Query.Where(predicate).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().AsEnumerable();
//        }

//        public IEnumerable<H> FindBy<H>(string predicate, object[] parameters, string order = "")
//        {
//            return Query.Where<T>(predicate, parameters).OrderBy(order).ProjectTo<H>().AsEnumerable();
//        }
//        public IEnumerable<H> FindBy<H>(string order, int pageIndex, int limit, string predicate, object[] parameters)
//        {
//            return Query.Where<T>(predicate, parameters).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().AsEnumerable();
//        }
//        public async Task<IEnumerable<H>> FindByAsync<H>(string predicate, object[] parameters, string order = "")
//        {
//            return await Query.Where<T>(predicate, parameters).OrderBy(order).ProjectTo<H>().ToListAsync();
//        }

//        public async Task<IEnumerable<H>> FindByAsync<H>(string order, int pageIndex, int limit, string predicate, object[] parameters)
//        {
//            return await Query.Where<T>(predicate, parameters).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().ToListAsync();
//        }
//        public async Task<IEnumerable<H>> FindByAsync<H>(Expression<Func<T, bool>> predicate, string order = "")
//        {
//            return await Query.Where(predicate).OrderBy(order).ProjectTo<H>().ToListAsync();
//        }
//        public async Task<IEnumerable<H>> FindByAsync<H>(string order, int pageIndex, int limit, Expression<Func<T, bool>> predicate)
//        {
//            return await Query.Where(predicate).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().ToListAsync();
//        }

//        public async Task<IEnumerable<H>> FindByAsync<H, TKey>(Expression<Func<T, TKey>> order, int pageIndex, int limit, Expression<Func<T, bool>> predicate)
//        {
//            return await Query.Where(predicate).OrderBy(order).Skip((pageIndex - 1) * limit).Take(limit).ProjectTo<H>().ToListAsync();
//        }
//        #endregion

//        public int CountAll()
//        {
//            return Query.Count();
//        }

//        public async Task<int> CountAllAsync()
//        {
//            return await Query.CountAsync();
//        }
//        public int Count(Expression<Func<T, bool>> predicate)
//        {
//            return Query.Where(predicate).Count();
//        }
//        public int Count(string predicate, object[] parameters)
//        {
//            return Query.Where(predicate, parameters).Count();
//        }
//        public bool Any(Expression<Func<T, bool>> predicate)
//        {
//            return Query.Where(predicate).Any();
//        }
//        public bool Any(string predicate, object[] parameters)
//        {
//            return Query.Where(predicate, parameters).Any();
//        }
//        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
//        {
//            return await Query.Where(predicate).AnyAsync();
//        }

//        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
//        {
//            return await Query.Where(predicate).CountAsync();
//        }
//        public async Task<bool> AnyAsync(string predicate, object[] parameters)
//        {
//            return await Query.Where(predicate, parameters).AnyAsync();
//        }

//        public async Task<int> CountAsync(string predicate, object[] parameters)
//        {
//            return await Query.Where(predicate, parameters).CountAsync();
//        }

//        public void Detach(T entity)
//        {
//            _context.Entry(entity).State = EntityState.Detached;
//        }

//        public void Detach(IEnumerable<T> entities)
//        {
//            foreach (var entity in entities)
//            {
//                _context.Entry(entity).State = EntityState.Detached;
//            }
//        }

//        public void Add(T entity)
//        {
//            entity.Id = Guid.NewGuid();
//            entity.Created = DateTime.Now;
//            entity.Modified = DateTime.Now;
//            entity.IsActivated = true;
//            if (entity is IAuditableEntity auditEntity)
//            {
//                auditEntity.CreatedBy = IdentityName;
//                auditEntity.ModifiedBy = IdentityName;
//                auditEntity.AppService = ServiceOwner;
//            }
//            _context.Set<T>().Add(entity);
//        }
//        public void Update(T entity)
//        {
//            entity.Modified = DateTime.Now;
//            var dbEntityEntry = _context.Entry(entity);
//            dbEntityEntry.State = EntityState.Modified;
//            dbEntityEntry.Property(x => x.Created).IsModified = false;

//            if (entity is IAuditableEntity auditEntity)
//            {
//                auditEntity.Modified = DateTime.Now;
//                auditEntity.ModifiedBy = IdentityName;
//                var auditableEntityEntry = _context.Entry(auditEntity);
//                auditableEntityEntry.Property(x => x.CreatedBy).IsModified = false;
//            }
//        }
//        public void Delete(T entity)
//        {
//            if (entity is ISoftDeleteEntity softDeleteEntity)
//            {
//                softDeleteEntity.IsDeleted = true;
//                softDeleteEntity.IsActivated = false;
//                softDeleteEntity.Modified = DateTime.Now;
//                var dbEntityEntry = _context.Entry(softDeleteEntity);
//                dbEntityEntry.Property(x => x.IsDeleted).IsModified = true;
//                dbEntityEntry.Property(x => x.Modified).IsModified = true;
//            }
//            else
//            {
//                var dbEntityEntry = _context.Entry(entity);
//                dbEntityEntry.State = EntityState.Deleted;
//            }
//        }

//        public void Add(IEnumerable<T> entities)
//        {
//            foreach (var entity in entities)
//            {
//                entity.Id = Guid.NewGuid();
//                entity.Created = DateTime.Now;
//                entity.Modified = DateTime.Now;
//                entity.IsActivated = true;
//                if (entity is IAuditableEntity auditEntity)
//                {
//                    auditEntity.CreatedBy = IdentityName;
//                    auditEntity.ModifiedBy = IdentityName;
//                }
//                if (entity is IServiceOwnerOnly serviceOwnerEntity)
//                {
//                    serviceOwnerEntity.AppService = ServiceOwner;
//                }
//            }
//            _context.Set<T>().AddRange(entities);
//        }

//        public void Update(IEnumerable<T> entities)
//        {
//            foreach (var entity in entities)
//            {
//                Update(entity);
//            }
//        }

//        public void Delete(IEnumerable<T> entities)
//        {
//            foreach (var entity in entities)
//            {
//                Delete(entity);
//            }
//        }

//        public int Commit()
//        {
//            EnsureAutoHistory();
//            return _context.SaveChanges();
//        }

//        public async Task<int> CommitAsync()
//        {
//            EnsureAutoHistory();
//            return await _context.SaveChangesAsync();
//        }

//        public void Load(T entity, params Expression<Func<T, object>>[] includeProperties)
//        {
//            foreach (var includeProperty in includeProperties)
//            {
//                _context.Entry(entity).Reference(includeProperty).Load();
//            }

//        }

//        private IQueryable<T> Query
//        {
//            get
//            {
//                var query = _context.Set<T>().AsQueryable();
//                if (!_forceAllItems)
//                {
//                    if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(T)))
//                    {
//                        query = query.Where(x => !((ISoftDeleteEntity)x).IsDeleted);
//                    }
//                    if (typeof(IOwnerOnly).IsAssignableFrom(typeof(T)))
//                    {
//                        query = query.Where(x => ((IOwnerOnly)x).CreatedBy == IdentityName);
//                    }
//                    if (typeof(IServiceOwnerOnly).IsAssignableFrom(typeof(T)))
//                    {
//                        query = query.Where(x => ((IServiceOwnerOnly)x).AppService == ServiceOwner);
//                    }
//                }
//                return query;
//            }
//        }
//        private string ServiceOwner
//        {
//            get
//            {
//                if (_ctxAccessor.ActionContext.HttpContext.User == null)
//                {
//                    return null;
//                }
//                return _ctxAccessor.ActionContext.HttpContext.User.Claims.Where(x => x.Type == "client_id").Select(x => x.Value).FirstOrDefault();
//            }
//        }
//        private string IdentityName
//        {
//            get
//            {
//                if (_ctxAccessor.ActionContext.HttpContext.User == null)
//                {
//                    return null;
//                }
//                return _ctxAccessor.ActionContext.HttpContext.User.Identity.Name;
//            }
//        }
//        private void EnsureAutoHistory()
//        {
//            if (typeof(T).IsAssignableFrom(typeof(IAutoHistory)))
//            {
//                _context.EnsureAutoHistory();
//            }
//        }

//    }
//}
