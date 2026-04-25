using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly StoreDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(StoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

         public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        void IRepository<T>.Add(T entity)
        {
            _dbSet.Add(entity);
        }

        void IRepository<T>.Update(T entity)
        {
           _dbSet.Update(entity);
        }

        void IRepository<T>.Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }

}

