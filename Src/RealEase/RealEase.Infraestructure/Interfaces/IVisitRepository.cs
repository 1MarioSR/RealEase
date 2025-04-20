using RealEase.Domain.Entities;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IVisitRepository
    {
        Task<List<Visit>> GetAllAsync();
        Task<Visit> GetByIdAsync(int id);
        Task<Visit> CreateAsync(Visit visit);
        Task<Visit> UpdateAsync(Visit visit);
        Task<bool> DeleteAsync(int id);
    }
}
