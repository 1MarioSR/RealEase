using Microsoft.AspNetCore.Mvc;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Interfaces;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("GetPayment/{id}")]
        public async Task<ActionResult<Payment>> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null) return NotFound(new { message = "Pago no encontrado." });

            var paymentResponse = new
            {
                payment.ContractId,
                payment.TenantId,
                payment.PaymentDate,
                payment.Amount,
                payment.PaymentMethod,
                payment.Status
            };

            return Ok(paymentResponse);
        }

        [HttpPost("AddPayment")]
        public async Task<ActionResult<NewPaymentResponse>> AddPayment(NewPaymentRequest request)
        {
            var payment = new Payment
            {
                ContractId = request.ContractId,
                TenantId = request.TenantId,
                PaymentDate = request.PaymentDate,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = request.Status
            };

            var newPayment = await _paymentRepository.CreateAsync(payment);

            return Ok(new NewPaymentResponse { Id = newPayment.Id });
        }

        [HttpPut("UpdatePayment/{id}")]
        public async Task<ActionResult<NewPaymentResponse>> UpdatePayment(int id, NewPaymentRequest request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null) return NotFound("Pago no encontrado.");

            existingPayment.ContractId = request.ContractId;
            existingPayment.TenantId = request.TenantId;
            existingPayment.PaymentDate = request.PaymentDate;
            existingPayment.Amount = request.Amount;
            existingPayment.PaymentMethod = request.PaymentMethod;
            existingPayment.Status = request.Status;

            var updatedPayment = await _paymentRepository.UpdateAsync(existingPayment);

            return Ok(new NewPaymentResponse { Id = updatedPayment.Id });
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null) return NotFound("Pago no encontrado.");

            var deleted = await _paymentRepository.DeleteAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el pago.");

            return Ok(new { message = "Pago eliminado exitosamente." });
        }
    }
}
