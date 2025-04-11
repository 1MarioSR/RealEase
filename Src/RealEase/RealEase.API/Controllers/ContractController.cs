using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Persistence.Context;
using System.Net;
using static RealEase.API.Requests.NewContractRequest;
using Contract = RealEase.Domain.Entities.Contract;



namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly RealEaseDbContext _context;

        public ContractController(RealEaseDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetContracts))]
        public async Task<ActionResult<List<Contract>>> GetContracts()
        {
            return await _context.Contracts.ToListAsync();
        }

        [HttpGet("GetContract/{Id}")]
        public async Task<ActionResult<ContractDto>> GetContract(int Id)
        {
            var ContractDb = await _context.Contracts.FindAsync(Id);

            if (ContractDb == null)
            {
                return NotFound();
            }

            var ContractDto = new ContractDto
            {
                Id = ContractDb.Id,
                ClientId = ContractDb.ClientId,
                AgentId = ContractDb.AgentId,
                PropertyId = ContractDb.PropertyId,
                StartDate = ContractDb.StartDate,
                EndDate = ContractDb.EndDate,
                MonthlyAmount = ContractDb.MonthlyAmount,
                Status = ContractDb.Status
            };

            return Ok(ContractDto);
        }


        [HttpPost("AddContract")]
        public async Task<ActionResult<NewContractResponse>> AddContract(NewContractRequest request)
        {

            //var userExists = await _context.Users.AnyAsync(u => u.Id == request.ClientId);

            //if (!userExists)
            //{
            //    return BadRequest(new { message = "User does not exist." });
            //}

            var ContractDb = new Contract
            {
                ClientId = request.ClientId,
                AgentId = request.AgentId,
                PropertyId = request.PropertyId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MonthlyAmount = request.MonthlyAmount,
                Status = request.Status
            };

            _context.Contracts.Add(ContractDb);
            await _context.SaveChangesAsync();

            return Ok(new NewContractResponse { Id = ContractDb.Id });
        }

        //var response = new NewContractResponse { Id = ContractDb.Id };
        //return response;
        //return ContractDb.Id;


        [HttpPut("UpdateContract/{id}")]
        public async Task<ActionResult<NewContractResponse>> UpdateContract(int Id, NewContractRequest request)
        {
            var ContractDb = await _context.Contracts.FindAsync(Id);

            if (ContractDb == null)
            {
                return NotFound();
            }

            ContractDb.ClientId = request.ClientId;
            ContractDb.AgentId = request.AgentId;
            ContractDb.PropertyId = request.PropertyId;
            ContractDb.StartDate = request.StartDate;
            ContractDb.EndDate = request.EndDate;
            ContractDb.MonthlyAmount = request.MonthlyAmount;
            ContractDb.Status = request.Status;

            _context.Contracts.Update(ContractDb);
            await _context.SaveChangesAsync();

            return Ok(new NewContractResponse { Id = ContractDb.Id });

        }

        [HttpDelete("DeleteContract/{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var ContractDb = await _context.Contracts.FindAsync(id);
            if (ContractDb == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(ContractDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
