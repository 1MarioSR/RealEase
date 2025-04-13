using Microsoft.AspNetCore.Mvc;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Interfaces;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitRepository _visitRepository;

        public VisitController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        [HttpGet("GetVisits")]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _visitRepository.GetAllAsync();
            return Ok(visits);
        }

        [HttpGet("GetVisit/{id}")]
        public async Task<ActionResult<Visit>> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit == null) return NotFound(new { message = "Visita no encontrada." });

            var visitResponse = new
            {
                visit.PropertyId,
                visit.UserId,
                visit.VisitDate,
                visit.Status,
                visit.Notes
            };

            return Ok(visitResponse);
        }

        [HttpPost("AddVisit")]
        public async Task<ActionResult<NewVisitResponse>> AddVisit(NewVisitRequest request)
        {
            var visit = new Visit
            {
                PropertyId = request.PropertyId,
                UserId = request.UserId,
                VisitDate = request.VisitDate,
                Status = request.Status,
                Notes = request.Notes
            };

            var newVisit = await _visitRepository.CreateAsync(visit);

            return Ok(new NewVisitResponse { Id = newVisit.Id });
        }

        [HttpPut("UpdateVisit/{id}")]
        public async Task<ActionResult<NewVisitResponse>> UpdateVisit(int id, NewVisitRequest request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            var existingVisit = await _visitRepository.GetByIdAsync(id);
            if (existingVisit == null) return NotFound("Visita no encontrada.");

            existingVisit.PropertyId = request.PropertyId;
            existingVisit.UserId = request.UserId;
            existingVisit.VisitDate = request.VisitDate;
            existingVisit.Status = request.Status;
            existingVisit.Notes = request.Notes;

            var updatedVisit = await _visitRepository.UpdateAsync(existingVisit);

            return Ok(new NewVisitResponse { Id = updatedVisit.Id });
        }

        [HttpDelete("DeleteVisit/{id}")]
        public async Task<ActionResult> DeleteVisit(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingVisit = await _visitRepository.GetByIdAsync(id);
            if (existingVisit == null) return NotFound("Visita no encontrada.");

            var deleted = await _visitRepository.DeleteAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar la visita.");

            return Ok(new { message = "Visita eliminada exitosamente." });
        }
    }
}
