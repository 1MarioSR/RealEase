using Microsoft.AspNetCore.Mvc;
using RealEase.Application.Dtos.Propertie;
using RealEase.Application.Services;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertieController : ControllerBase
    {
        private readonly PropertieService _propertieService;

        public PropertieController(PropertieService propertieService)
        {
            _propertieService = propertieService;
        }

        [HttpGet("GetProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertieService.GetAllPropertiesAsync(string.Empty);
            return Ok(properties);
        }

        [HttpGet("GetPropertie/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var propertie = await _propertieService.GetPropertieByIdAsync(id);
            if (propertie == null) return NotFound(new { message = "Propiedad no encontrada." });

            var propertieResponse = new
            {
                propertie.Id,
                propertie.Title,
                propertie.Image,
                propertie.Description,
                propertie.Address,
                propertie.Price,
                propertie.PropertyType,
                propertie.Status,
                propertie.OwnerId
            };

            return Ok(propertieResponse);
        }

        [HttpPost("AddPropertie")]
        public async Task<IActionResult> AddPropertie(PropertieDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertieId = await _propertieService.AddPropertieAsync(request);
            if (propertieId == 0)
                return StatusCode(500, "No se pudo crear la propiedad.");

            return Ok(new { Id = propertieId });
        }

        [HttpPut("UpdatePropertie/{id}")]
        public async Task<IActionResult> UpdatePropertie(int id, PropertieDto request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            request.Id = id;

            var existingPropertie = await _propertieService.GetPropertieByIdAsync(id);
            if (existingPropertie == null) return NotFound("Propiedad no encontrada.");

            var updated = await _propertieService.UpdatePropertieAsync(request);
            if (!updated) return StatusCode(500, "No se pudo actualizar la propiedad.");

            return Ok(new { Id = id, message = "Propiedad actualizada exitosamente." });
        }

        [HttpDelete("DeletePropertie/{id}")]
        public async Task<IActionResult> DeletePropertie(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingPropertie = await _propertieService.GetPropertieByIdAsync(id);
            if (existingPropertie == null) return NotFound("Propiedad no encontrada.");

            var deleted = await _propertieService.DeletePropertieAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar la propiedad.");

            return Ok(new { message = "Propiedad eliminada exitosamente." });
        }
    }
}