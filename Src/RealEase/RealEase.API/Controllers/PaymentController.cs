using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Persistence.Context;
using System.Net;
using static RealEase.API.Requests.NewPaymentRequest;
using Payment = RealEase.Domain.Entities.Payment;



namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly RealEaseDbContext _context;

        public PaymentController(RealEaseDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetPayments))]
        public async Task<ActionResult<List<Payment>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        [HttpGet("GetPayment/{Id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int Id)
        {
            var PaymentDb = await _context.Payments.FindAsync(Id);

            if (PaymentDb == null)
            {
                return NotFound();
            }

            var PaymentDto = new PaymentDto
            {
                Id = PaymentDb.Id,
                ContractId = PaymentDb.ContractId,
                TenantId = PaymentDb.TenantId,
                PaymentDate = PaymentDb.PaymentDate,
                Amount = PaymentDb.Amount,
                PaymentMethod = PaymentDb.PaymentMethod,
                Status = PaymentDb.Status
            };

            return Ok(PaymentDto);
        }


        [HttpPost("AddPayment")]
        public async Task<ActionResult<NewPaymentResponse>> AddPayment(NewPaymentRequest request)
        {

            //var userExists = await _context.Users.AnyAsync(u => u.Id == request.ClientId);

            //if (!userExists)
            //{
            //    return BadRequest(new { message = "User does not exist." });
            //}

            var PaymentDb = new Payment();
            
            PaymentDb.ContractId = request.ContractId;
            PaymentDb.TenantId = request.TenantId;
            PaymentDb.PaymentDate = request.PaymentDate;
            PaymentDb.Amount = request.Amount;
            PaymentDb.PaymentMethod = request.PaymentMethod;
            PaymentDb.Status = request.Status;
           

            _context.Payments.Add(PaymentDb);
            await _context.SaveChangesAsync();

            return Ok(new NewPaymentResponse { Id = PaymentDb.Id });
        }

        //var response = new NewPaymentResponse { Id = PaymentDb.Id };
        //return response;
        //return PaymentDb.Id;


        [HttpPut("UpdatePayment/{id}")]
        public async Task<ActionResult<NewPaymentResponse>> UpdatePayment(int Id, NewPaymentRequest request)
        {
            var PaymentDb = await _context.Payments.FindAsync(Id);

            if (PaymentDb == null)
            {
                return NotFound();
            }

            PaymentDb.ContractId = request.ContractId;
            PaymentDb.TenantId = request.TenantId;
            PaymentDb.PaymentDate = request.PaymentDate;
            PaymentDb.Amount = request.Amount;
            PaymentDb.PaymentMethod = request.PaymentMethod;
            PaymentDb.Status = request.Status;

            _context.Payments.Update(PaymentDb);
            await _context.SaveChangesAsync();

            return Ok(new NewPaymentResponse { Id = PaymentDb.Id });

        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var PaymentDb = await _context.Payments.FindAsync(id);
            if (PaymentDb == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(PaymentDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
