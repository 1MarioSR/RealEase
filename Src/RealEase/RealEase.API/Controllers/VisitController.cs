using Microsoft.AspNetCore.Mvc;
using RealEase.Application.Dtos.Visit;
using RealEase.Application.Services;
using RealEase.Domain.Entities;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly VisitService _visitService;

        public VisitController(VisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet("GetVisits")]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _visitService.GetAllVisitsAsync();
            return Ok(visits);
        }

        [HttpGet("GetVisit/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var visit = await _visitService.GetVisitByIdAsync(id);
            if (visit == null) return NotFound(new { message = "Visita no encontrada." });

            var visitResponse = new
            {
                visit.Id,
                visit.PropertyId,
                visit.UserId,
                visit.VisitDate,
                visit.Status,
                visit.Notes
            };

            return Ok(visitResponse);
        }

        [HttpPost("AddVisit")]
        public async Task<IActionResult> AddVisit(VisitDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var visitId = await _visitService.AddVisitAsync(request);
            if (visitId == 0)
                return StatusCode(500, "No se pudo crear la visita.");

            return Ok(new { Id = visitId });
        }

        [HttpPut("UpdateVisit/{id}")]
        public async Task<IActionResult> UpdateVisit(int id, VisitDto request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");
            if (id != request.Id) return BadRequest("El ID de ruta y el ID de la visita no coinciden.");

            var existingVisit = await _visitService.GetVisitByIdAsync(id);
            if (existingVisit == null) return NotFound("Visita no encontrada.");

            var updated = await _visitService.UpdateVisitAsync(request);
            if (!updated) return StatusCode(500, "No se pudo actualizar la visita.");

            return Ok(new { Id = id, message = "Visita actualizada exitosamente." });
        }

        [HttpDelete("DeleteVisit/{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingVisit = await _visitService.GetVisitByIdAsync(id);
            if (existingVisit == null) return NotFound("Visita no encontrada.");

            var deleted = await _visitService.DeleteVisitAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar la visita.");

            return Ok(new { message = "Visita eliminada exitosamente." });
        }
    }
}