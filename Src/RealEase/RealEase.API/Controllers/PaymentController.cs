using Microsoft.AspNetCore.Mvc;
using RealEase.Application.Dtos.Payment;
using RealEase.Application.Services;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync(string.Empty);
            return Ok(payments);
        }

        [HttpGet("GetPayment/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound(new { message = "Pago no encontrado." });

            var paymentResponse = new
            {
                payment.Id,
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
        public async Task<IActionResult> AddPayment(PaymentDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentId = await _paymentService.AddPaymentAsync(request);
            if (paymentId == 0)
                return StatusCode(500, "No se pudo crear el pago.");

            return Ok(new { Id = paymentId });
        }

        [HttpPut("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDto request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            // Asignar el ID de la ruta al objeto request directamente
            request.Id = id;

            var existingPayment = await _paymentService.GetPaymentByIdAsync(id);
            if (existingPayment == null) return NotFound("Pago no encontrado.");

            var updated = await _paymentService.UpdatePaymentAsync(request);
            if (!updated) return StatusCode(500, "No se pudo actualizar el pago.");

            return Ok(new { Id = id, message = "Pago actualizado exitosamente." });
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingPayment = await _paymentService.GetPaymentByIdAsync(id);
            if (existingPayment == null) return NotFound("Pago no encontrado.");

            var deleted = await _paymentService.DeletePaymentAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el pago.");

            return Ok(new { message = "Pago eliminado exitosamente." });
        }
    }
}