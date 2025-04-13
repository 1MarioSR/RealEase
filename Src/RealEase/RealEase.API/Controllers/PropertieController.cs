using Microsoft.AspNetCore.Mvc;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Interfaces;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertieController : ControllerBase
    {
        private readonly IPropertieRepository _propertieRepository;

        public PropertieController(IPropertieRepository propertieRepository)
        {
            _propertieRepository = propertieRepository;
        }

        [HttpGet("GetProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertieRepository.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("GetPropertie/{id}")]
        public async Task<ActionResult<Propertie>> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var propertie = await _propertieRepository.GetByIdAsync(id);
            if (propertie == null) return NotFound(new { message = "Propiedad no encontrada." });

            var propertieResponse = new
            {
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
        public async Task<ActionResult<NewPropertieResponse>> AddPropertie(NewPropertieRequest request)
        {
            var propertie = new Propertie
            {
                Title = request.Title,
                Image = request.Image,
                Description = request.Description,
                Address = request.Address,
                Price = request.Price,
                PropertyType = request.PropertyType,
                Status = request.Status,
                OwnerId = request.OwnerId
            };

            var newPropertie = await _propertieRepository.CreateAsync(propertie);

            return Ok(new NewPropertieResponse { Id = newPropertie.Id });
        }

        [HttpPut("UpdatePropertie/{id}")]
        public async Task<ActionResult<NewPropertieResponse>> UpdatePropertie(int id, NewPropertieRequest request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");

            var existingPropertie = await _propertieRepository.GetByIdAsync(id);
            if (existingPropertie == null) return NotFound("Propiedad no encontrada.");

            existingPropertie.Title = request.Title;
            existingPropertie.Image = request.Image;
            existingPropertie.Description = request.Description;
            existingPropertie.Address = request.Address;
            existingPropertie.Price = request.Price;
            existingPropertie.PropertyType = request.PropertyType;
            existingPropertie.Status = request.Status;
            existingPropertie.OwnerId = request.OwnerId;

            var updatedPropertie = await _propertieRepository.UpdateAsync(existingPropertie);

            return Ok(new NewPropertieResponse { Id = updatedPropertie.Id });
        }

        [HttpDelete("DeletePropertie/{id}")]
        public async Task<ActionResult> DeletePropertie(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingPropertie = await _propertieRepository.GetByIdAsync(id);
            if (existingPropertie == null) return NotFound("Propiedad no encontrada.");



            var deleted = await _propertieRepository.DeleteAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar la propiedad.");

            return Ok(new { message = "Propiedad eliminada exitosamente." });
        }
    }
}
