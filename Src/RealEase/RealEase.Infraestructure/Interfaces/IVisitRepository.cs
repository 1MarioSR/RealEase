using RealEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IVisitRepository
    {
        Task<List<Visit>> GetAllAsync();
        Task<Visit> GetByIdAsync(int id);
        Task<Visit> CreateAsync(Visit visit);
        Task<Visit> UpdateAsync(Visit visit);
        Task<bool> DeleteAsync(int id);
        Task<List<Visit>> GetVisitsByPropertyIdAsync(int propertyId);
        Task<List<Visit>> GetVisitsByUserIdAsync(int userId);
        Task<List<Visit>> GetUpcomingVisitsAsync();
    }
}
