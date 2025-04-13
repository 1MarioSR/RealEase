using RealEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllAsync();
        Task<Contract> GetByIdAsync(int id);
        Task<Contract> CreateAsync(Contract contract);
        Task<Contract> UpdateAsync(Contract contract);
        Task<bool> DeleteAsync(int id);
        Task<List<Contract>> GetContractsByClientIdAsync(int clientId);
        Task<List<Contract>> GetContractsByAgentIdAsync(int agentId);
        Task<List<Contract>> GetContractsByStatusAsync(string status);
        Task<List<Contract>> GetActiveContractsAsync();
    }
}
