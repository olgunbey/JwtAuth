using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IRepository;
using JsonWebToken.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity,new()
    {
        protected readonly AppDbContext appContexts;
        protected DbSet<T> dbSet;
        public GenericRepository(AppDbContext appContext)
        {
            this.appContexts = appContext;
            dbSet = appContexts.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            Task.FromResult(dbSet.Remove(entity));
        }

        public async Task<T> Get(T entity)
        {
           return await dbSet.FindAsync(entity.Id);
        }

        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
         return Task.FromResult(dbSet.Where(predicate).AsEnumerable());
        }

        public async Task UpdateAsync(T entity)
        {
            Task.FromResult(dbSet.Update(entity));
        }
    }
}
