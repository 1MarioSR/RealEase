﻿using RealEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(int id);
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment> UpdateAsync(Payment payment);
        Task<bool> DeleteAsync(int id);
        Task<List<Payment>> GetPaymentsByTenantIdAsync(int tenantId);
        Task<List<Payment>> GetPaymentsByContractIdAsync(int contractId);
        Task<List<Payment>> GetPaymentsByStatusAsync(string status);
    }
}
