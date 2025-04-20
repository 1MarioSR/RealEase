using Microsoft.AspNetCore.Mvc;
using RealEase.Application.Dtos.Contract;
using RealEase.Application.Services;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly ContractService _contractService;
        
        public ContractController(ContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("GetContracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        [HttpGet("GetContract/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");
            
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null) return NotFound(new { message = "Contrato no encontrado." });
            
            var contractResponse = new
            {
                contract.Id,
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
        public async Task<IActionResult> AddContract(ContractDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var contractId = await _contractService.AddContractAsync(request);
            if (contractId == 0)
                return StatusCode(500, "No se pudo crear el contrato.");
                
            return Ok(new { Id = contractId });
        }
        
        [HttpPut("UpdateContract/{id}")]
        public async Task<IActionResult> UpdateContract(int id, ContractDto request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");
            if (id != request.Id) return BadRequest("El ID de ruta y el ID del contrato no coinciden.");
            
            var existingContract = await _contractService.GetContractByIdAsync(id);
            if (existingContract == null) return NotFound("Contrato no encontrado.");
            
            var updated = await _contractService.UpdateContractAsync(request);
            if (!updated) return StatusCode(500, "No se pudo actualizar el contrato.");
            
            return Ok(new { Id = id, message = "Contrato actualizado exitosamente." });
        }
        
        [HttpDelete("DeleteContract/{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");
            
            var existingContract = await _contractService.GetContractByIdAsync(id);
            if (existingContract == null) return NotFound("Contrato no encontrado.");
            
            var deleted = await _contractService.DeleteContractAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el contrato.");
            
            return Ok(new { message = "Contrato eliminado exitosamente." });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredContracts([FromQuery] DateTime? startdate, [FromQuery] int? contractid, [FromQuery] string? status)
        {
            var payments = await _contractService.GetFilteredContractsAsync(startdate, contractid, status);
            return Ok(payments);
        }
    }
}