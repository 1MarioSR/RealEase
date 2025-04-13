using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Repository;
using RealEase.Persistence.Context;
using System.Linq.Expressions;

namespace RealEase.Infrastructure.Core
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly RealEaseDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(RealEaseDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
