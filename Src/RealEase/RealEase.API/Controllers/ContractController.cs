using Microsoft.AspNetCore.Mvc;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Interfaces;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;

        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        [HttpGet("GetContracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractRepository.GetAllAsync();
            return Ok(contracts);
        }

        [HttpGet("GetContract/{id}")]
        public async Task<ActionResult<Contract>> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null) return NotFound(new { message = "Contrato no encontrado." });

            var contractResponse = new
            {
                contract.ClientId,
                contract.AgentId,
                contract.PropertyId,
                contract.StartDate,
                contract.EndDate,
                contract.MonthlyAmount,
                contract.Status
            };

            return Ok(contractResponse);
        }

        [HttpPost("AddContract")]
        public async Task<ActionResult<NewContractResponse>> AddContract(NewContractRequest request)
        {
            var contract = new Contract
            {
                ClientId = request.ClientId,
                AgentId = request.AgentId,
                PropertyId = request.PropertyId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MonthlyAmount = request.MonthlyAmount,
                Status = request.Status
            };

            var newContract = await _contractRepository.CreateAsync(contract);

            return Ok(new NewContractResponse { Id = newContract.Id });
        }

        [HttpPut("UpdateContract/{id}")]
        public async Task<ActionResult<NewContractResponse>> UpdateContract(int id, NewContractRequest request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            var existingContract = await _contractRepository.GetByIdAsync(id);
            if (existingContract == null) return NotFound("Contrato no encontrado.");

            existingContract.ClientId = request.ClientId;
            existingContract.AgentId = request.AgentId;
            existingContract.PropertyId = request.PropertyId;
            existingContract.StartDate = request.StartDate;
            existingContract.EndDate = request.EndDate;
            existingContract.MonthlyAmount = request.MonthlyAmount;
            existingContract.Status = request.Status;

            var updatedContract = await _contractRepository.UpdateAsync(existingContract);

            return Ok(new NewContractResponse { Id = updatedContract.Id });
        }

        [HttpDelete("DeleteContract/{id}")]
        public async Task<ActionResult> DeleteContract(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingContract = await _contractRepository.GetByIdAsync(id);
            if (existingContract == null) return NotFound("Contrato no encontrado.");

            var deleted = await _contractRepository.DeleteAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el contrato.");

            return Ok(new { message = "Contrato eliminado exitosamente." });
        }
    }
}
