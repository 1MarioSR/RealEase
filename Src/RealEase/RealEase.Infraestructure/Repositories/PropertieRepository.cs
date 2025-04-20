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

    public async Task<List<Propertie>> GetFilteredPropertiesAsync(string? city, decimal? price, string? type, string? status)
    {
        var query = _context.Properties.AsQueryable();

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(u => u.Address.Contains(city));
        }

        if (price.HasValue)
        {
            query = query.Where(u => u.Price == price.Value);
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(u => u.PropertyType.Contains(type));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(u => u.Status.Contains(status));
        }

        return await query.ToListAsync();
    }

}
       

