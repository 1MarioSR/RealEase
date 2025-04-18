using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Exceptions;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;

namespace RealEase.Infrastructure.Repositories
{
    public class VisitRepository : BaseRepository<Visit>, IVisitRepository
    {
        public VisitRepository(RealEaseDbContext context) : base(context) { }

        public async Task<Visit> GetByIdAsync(int id)
        {
            var visit = await _dbSet.FirstOrDefaultAsync(v => v.Id == id);
            if (visit == null) throw new VisitException("Visita no encontrada.");
            return visit;
        }

        public async Task<Visit> CreateAsync(Visit visit)
        {
            await _dbSet.AddAsync(visit);
            await _context.SaveChangesAsync();
            return visit;
        }

        public async Task<Visit> UpdateAsync(Visit visit)
        {
            var existingVisit = await GetByIdAsync(visit.Id);
            if (existingVisit == null) throw new VisitException("Visita no encontrada.");

            _context.Entry(existingVisit).CurrentValues.SetValues(visit);
            await _context.SaveChangesAsync();

            return existingVisit;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var visit = await GetByIdAsync(id);
            if (visit == null) return false;

            _dbSet.Remove(visit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Visit>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Visit>> GetVisitsByPropertyIdAsync(int propertyId)
        {
            return await _dbSet.Where(v => v.PropertyId == propertyId).ToListAsync();
        }

        public async Task<List<Visit>> GetVisitsByUserIdAsync(int userId)
        {
            return await _dbSet.Where(v => v.UserId == userId).ToListAsync();
        }

        public async Task<List<Visit>> GetUpcomingVisitsAsync()
        {
            return await _dbSet.Where(v => v.VisitDate >= DateTime.UtcNow).ToListAsync();
        }
    }
}
