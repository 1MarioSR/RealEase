using RealEase.Application.Dtos.Payment;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEase.Application.Services
{
    public class PaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(UnitOfWork unitOfWork, PaymentRepository paymentRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync(string filter)
        {
            var payments = await _paymentRepository.GetAllAsync();

            var paymentDtos = new List<PaymentDto>();
            foreach (var payment in payments)
            {
                paymentDtos.Add(new PaymentDto
                {
                    Id = payment.Id,
                    ContractId = payment.ContractId,
                    TenantId = payment.TenantId,
                    PaymentDate = payment.PaymentDate,
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status
                });
            }

            return paymentDtos;
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.Id,
                ContractId = payment.ContractId,
                TenantId = payment.TenantId,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
            };
        }

        public async Task<int> AddPaymentAsync(PaymentDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var payment = new Payment
                {
                    ContractId = dto.ContractId,
                    TenantId = dto.TenantId,
                    PaymentDate = dto.PaymentDate,
                    Amount = dto.Amount,
                    PaymentMethod = dto.PaymentMethod,
                    Status = dto.Status
                };

                var newPayment = await _paymentRepository.CreateAsync(payment);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return newPayment.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return 0;
            }
        }

        public async Task<bool> UpdatePaymentAsync(PaymentDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var payment = await _paymentRepository.GetByIdAsync(dto.Id);
                if (payment == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                payment.ContractId = dto.ContractId;
                payment.TenantId = dto.TenantId;
                payment.PaymentDate = dto.PaymentDate;
                payment.Amount = dto.Amount;
                payment.PaymentMethod = dto.PaymentMethod;
                payment.Status = dto.Status;

                var updatedPayment = await _paymentRepository.UpdateAsync(payment);
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

        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var payment = await _paymentRepository.GetByIdAsync(id);
                if (payment == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                var deleted = await _paymentRepository.DeleteAsync(id);
                if (!deleted)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

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

