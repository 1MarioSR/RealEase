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

        public async Task<List<Visit>> GetFilteredVisitsAsync(DateTime? visitdate, int? propertyid, int? clientid, string? status)
        {
            var query = _context.Visits.AsQueryable();

            if (visitdate.HasValue)
                query = query.Where(u => u.VisitDate.Date == visitdate.Value.Date);

            if (propertyid.HasValue)
                query = query.Where(u => u.PropertyId == propertyid.Value);

            if (clientid.HasValue)
                query = query.Where(u => u.UserId == clientid.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(u => u.Status == status);

            return await query.ToListAsync();
        }


    }
}
       

