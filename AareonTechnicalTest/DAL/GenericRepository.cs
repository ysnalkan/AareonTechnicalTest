using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AareonTechnicalTest.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
        Task<bool> Update(int id,T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected string CurrentUser => _accessor.HttpContext.User.Identity.Name;
        protected ApplicationContext context;
        protected IHttpContextAccessor _accessor;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationContext context,IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {

            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                var audit = (IAuditEntity)entity;
                audit.SetCreator(CurrentUser, DateTime.UtcNow);
            }
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Update(int id ,T entity)
        {
            throw new NotImplementedException();
        }
    }
}
