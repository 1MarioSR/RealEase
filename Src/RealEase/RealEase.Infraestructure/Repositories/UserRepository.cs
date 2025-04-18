using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Exceptions;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;

namespace RealEase.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RealEaseDbContext context) : base(context) { }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new UserException("Usuario no encontrado.");
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) throw new UserException("Usuario no encontrado con ese correo.");
            return user;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null) throw new UserException("Usuario no encontrado.");

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return false;

            _dbSet.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            return await _dbSet.Where(u => u.Role == role).ToListAsync();
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
            return await _dbSet.Where(u => u.IsActive).ToListAsync();
        }
    }
}
