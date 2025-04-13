using RealEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IPropertieRepository
    {
        Task<List<Propertie>> GetAllAsync();
        Task<Propertie> GetByIdAsync(int id);
        Task<Propertie> CreateAsync(Propertie propertie);
        Task<Propertie> UpdateAsync(Propertie propertie);
        Task<bool> DeleteAsync(int id);
        Task<List<Propertie>> GetPropertiesByOwnerIdAsync(int ownerId);
        Task<List<Propertie>> GetPropertiesByStatusAsync(string status);
        Task<List<Propertie>> GetPropertiesByTypeAsync(string propertyType);
    }
}
