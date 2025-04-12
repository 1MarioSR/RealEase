using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Persistence.Context;
using System.Net;
using static RealEase.API.Requests.NewPropertieRequest;
using Propertie = RealEase.Domain.Entities.Propertie;



namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertieController : ControllerBase
    {
        private readonly RealEaseDbContext _context;

        public PropertieController(RealEaseDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetProperties))]
        public async Task<ActionResult<List<Propertie>>> GetProperties()
        {
            return await _context.Properties.ToListAsync();
        }

        [HttpGet("GetPropertie/{Id}")]
        public async Task<ActionResult<PropertieDto>> GetPropertie(int Id)
        {
            var PropertieDb = await _context.Properties.FindAsync(Id);

            if (PropertieDb == null)
            {
                return NotFound();
            }

            var PropertieDto = new PropertieDto
            {
                Id = PropertieDb.Id,
                Title = PropertieDb.Title,
                Image = PropertieDb.Image,
                Description = PropertieDb.Description,
                Address = PropertieDb.Address,
                Price = PropertieDb.Price,
                PropertyType = PropertieDb.PropertyType,
                Status = PropertieDb.Status,
                OwnerId = PropertieDb.OwnerId
            };

            return Ok(PropertieDto);
        }


        [HttpPost("AddPropertie")]
        public async Task<ActionResult<NewPropertieResponse>> AddPropertie(NewPropertieRequest request)
        {

            var userExists = await _context.Users.AnyAsync(u => u.Id == request.OwnerId);

            if (!userExists)
            {
                return BadRequest(new { message = "User does not exist." });
            }

            var propertieDb = new Propertie
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

            _context.Properties.Add(propertieDb);
            await _context.SaveChangesAsync();

            return Ok(new NewPropertieResponse { Id = propertieDb.Id });
        }

            //var response = new NewPropertieResponse { Id = PropertieDb.Id };
            //return response;
            //return PropertieDb.Id;
        

        [HttpPut("UpdatePropertie/{id}")]
        public async Task<ActionResult<NewPropertieResponse>> UpdatePropertie(int Id, NewPropertieRequest request)
        {
            var PropertieDb = await _context.Properties.FindAsync(Id);

            if (PropertieDb == null)
            {
                return NotFound();
            }

            PropertieDb.Title = request.Title;
            PropertieDb.Image = request.Image;
            PropertieDb.Description = request.Description;
            PropertieDb.Address = request.Address;
            PropertieDb.Price = request.Price;
            PropertieDb.PropertyType = request.PropertyType;
            PropertieDb.Status = request.Status;
            PropertieDb.OwnerId = request.OwnerId;

            _context.Properties.Update(PropertieDb);
            await _context.SaveChangesAsync();

            return Ok(new NewPropertieResponse { Id = PropertieDb.Id });

        }

        [HttpDelete("DeletePropertie/{id}")]
        public async Task<IActionResult> DeletePropertie(int id)
        {
            var PropertieDb = await _context.Properties.FindAsync(id);
            if (PropertieDb == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(PropertieDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
