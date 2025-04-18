using RealEase.Infrastructure.Core;
using RealEase.Application.Dtos.Contract;
using RealEase.Domain.Entities;

namespace RealEase.Application.Services
{
    public class ContractService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ContractRepository _contractRepository;

        public ContractService(UnitOfWork unitOfWork, ContractRepository contractRepository)
        {
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
        }

        
        public async Task<List<ContractDto>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllAsync();

            var contractDtos = new List<ContractDto>();
            foreach (var contract in contracts)
            {
                contractDtos.Add(new ContractDto
                {
                    Id = contract.Id,
                    ClientId = contract.ClientId,
                    AgentId = contract.AgentId,
                    PropertyId = contract.PropertyId,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    MonthlyAmount = contract.MonthlyAmount,
                    Status = contract.Status
                });
            }

            return contractDtos;
        }

       
        public async Task<ContractDto> GetContractByIdAsync(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null) return null;

            return new ContractDto
            {
                Id = contract.Id,
                ClientId = contract.ClientId,
                AgentId = contract.AgentId,
                PropertyId = contract.PropertyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                MonthlyAmount = contract.MonthlyAmount,
                Status = contract.Status
            };
        }

        
        public async Task<int> AddContractAsync(ContractDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var contract = new Contract
                {
                    ClientId = dto.ClientId,
                    AgentId = dto.AgentId,
                    PropertyId = dto.PropertyId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    MonthlyAmount = dto.MonthlyAmount,
                    Status = dto.Status
                };

                await _contractRepository.AddAsync(contract);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return contract.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return 0;
            }
        }

        
        public async Task<bool> UpdateContractAsync(ContractDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var contract = await _contractRepository.GetByIdAsync(dto.Id);
                if (contract == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                contract.ClientId = dto.ClientId;
                contract.AgentId = dto.AgentId;
                contract.PropertyId = dto.PropertyId;
                contract.StartDate = dto.StartDate;
                contract.EndDate = dto.EndDate;
                contract.MonthlyAmount = dto.MonthlyAmount;
                contract.Status = dto.Status;

                await _contractRepository.UpdateAsync(contract);
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

        
        public async Task<bool> DeleteContractAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var contract = await _contractRepository.GetByIdAsync(id);
                if (contract == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                await _contractRepository.DeleteAsync(id);
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
