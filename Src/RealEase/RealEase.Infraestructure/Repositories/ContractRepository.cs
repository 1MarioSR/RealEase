using Microsoft.EntityFrameworkCore;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Exceptions;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;

public class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(RealEaseDbContext context) : base(context) { }

    public async Task<List<Contract>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Contract> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Id == id)
               ?? throw new ContractException("Contrato no encontrado.");
    }

    public async Task<Contract> CreateAsync(Contract contract)
    {
        await _dbSet.AddAsync(contract);
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<Contract> UpdateAsync(Contract contract)
    {
        var existingContract = await GetByIdAsync(contract.Id);
        _context.Entry(existingContract).CurrentValues.SetValues(contract);
        await _context.SaveChangesAsync();
        return existingContract;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contract = await GetByIdAsync(id);
        if (contract == null) return false;

        _dbSet.Remove(contract);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Contract>> GetContractsByClientIdAsync(int clientId)
    {
        return await _dbSet.Where(c => c.ClientId == clientId).ToListAsync();
    }

    public async Task<List<Contract>> GetContractsByAgentIdAsync(int agentId)
    {
        return await _dbSet.Where(c => c.AgentId == agentId).ToListAsync();
    }

    public async Task<List<Contract>> GetContractsByStatusAsync(string status)
    {
        return await _dbSet.Where(c => c.Status == status).ToListAsync();
    }

    public async Task<List<Contract>> GetActiveContractsAsync()
    {
        return await _dbSet.Where(c => c.Status == "Activo").ToListAsync();
    }
}
