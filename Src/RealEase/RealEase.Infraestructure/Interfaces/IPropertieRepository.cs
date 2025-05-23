﻿using RealEase.Domain.Entities;

namespace RealEase.Infrastructure.Interfaces
{
    public interface IPropertieRepository
    {
        Task<List<Propertie>> GetAllAsync();
        Task<Propertie> GetByIdAsync(int id);
        Task<Propertie> CreateAsync(Propertie propertie);
        Task<Propertie> UpdateAsync(Propertie propertie);
        Task<bool> DeleteAsync(int id);
    }
}
