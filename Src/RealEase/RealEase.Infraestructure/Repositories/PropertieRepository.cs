using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Exceptions;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;
public class PropertieRepository : BaseRepository<Propertie>, IPropertieRepository
{
    public PropertieRepository(RealEaseDbContext context) : base(context) { }

    public async Task<List<Propertie>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Propertie> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id)
               ?? throw new PropertieException("Propiedad no encontrada.");
    }

    public async Task<Propertie> CreateAsync(Propertie propertie)
    {
        await _dbSet.AddAsync(propertie);
        await _context.SaveChangesAsync();
        return propertie;
    }

    public async Task<Propertie> UpdateAsync(Propertie propertie)
    {
        var existingPropertie = await GetByIdAsync(propertie.Id);
        _context.Entry(existingPropertie).CurrentValues.SetValues(propertie);
        await _context.SaveChangesAsync();
        return existingPropertie;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var propertie = await GetByIdAsync(id);
        if (propertie == null) return false;

        _dbSet.Remove(propertie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Propertie>> GetPropertiesByOwnerIdAsync(int ownerId)
    {
        return await _dbSet.Where(p => p.OwnerId == ownerId).ToListAsync();
    }

    public async Task<List<Propertie>> GetPropertiesByStatusAsync(string status)
    {
        return await _dbSet.Where(p => p.Status == status).ToListAsync();
    }

    public async Task<List<Propertie>> GetPropertiesByTypeAsync(string propertyType)
    {
        return await _dbSet.Where(p => p.PropertyType == propertyType).ToListAsync();
    }
}
