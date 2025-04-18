using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Repositories;
using RealEase.Application.Dtos.Visit;
using RealEase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Application.Services
{
    public class VisitService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly VisitRepository _visitRepository;

        public VisitService(UnitOfWork unitOfWork, VisitRepository visitRepository)
        {
            _unitOfWork = unitOfWork;
            _visitRepository = visitRepository;
        }

        
        public async Task<List<VisitDto>> GetAllVisitsAsync()
        {
            var visits = await _visitRepository.GetAllAsync();
            var visitDtos = new List<VisitDto>();

            foreach (var visit in visits)
            {
                visitDtos.Add(new VisitDto
                {
                    Id = visit.Id,
                    PropertyId = visit.PropertyId,
                    UserId = visit.UserId,
                    VisitDate = visit.VisitDate,
                    Status = visit.Status,
                    Notes = visit.Notes
                });
            }

            return visitDtos;
        }

        
        public async Task<VisitDto> GetVisitByIdAsync(int id)
        {
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit == null) return null;

            return new VisitDto
            {
                Id = visit.Id,
                PropertyId = visit.PropertyId,
                UserId = visit.UserId,
                VisitDate = visit.VisitDate,
                Status = visit.Status,
                Notes = visit.Notes
            };
        }

       
        public async Task<int> AddVisitAsync(VisitDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var visit = new Visit
                {
                    PropertyId = dto.PropertyId,
                    UserId = dto.UserId,
                    VisitDate = dto.VisitDate,
                    Status = dto.Status,
                    Notes = dto.Notes
                };

                await _visitRepository.AddAsync(visit);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return visit.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return 0;
            }
        }

        
        public async Task<bool> UpdateVisitAsync(VisitDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var visit = await _visitRepository.GetByIdAsync(dto.Id);
                if (visit == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                visit.PropertyId = dto.PropertyId;
                visit.UserId = dto.UserId;
                visit.VisitDate = dto.VisitDate;
                visit.Status = dto.Status;
                visit.Notes = dto.Notes;

                await _visitRepository.UpdateAsync(visit);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        
        public async Task<bool> DeleteVisitAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var visit = await _visitRepository.GetByIdAsync(id);
                if (visit == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                await _visitRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }
    }
}
