using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Persistence.Context;
using System.Net;
using static RealEase.API.Requests.NewVisitRequest;
using Visit = RealEase.Domain.Entities.Visit;



namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly RealEaseDbContext _context;

        public VisitController(RealEaseDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(GetVisits))]
        public async Task<ActionResult<List<Visit>>> GetVisits()
        {
            return await _context.Visits.ToListAsync();
        }

        [HttpGet("GetVisit/{Id}")]
        public async Task<ActionResult<VisitDto>> GetVisit(int Id)
        {
            var VisitDb = await _context.Visits.FindAsync(Id);

            if (VisitDb == null)
            {
                return NotFound();
            }

            var VisitDto = new VisitDto
            {
                Id = VisitDb.Id,
                PropertyId = VisitDb.PropertyId,
                UserId = VisitDb.UserId,
                VisitDate = VisitDb.VisitDate,
                Status = VisitDb.Status,
                Notes = VisitDb.Notes
               
            };

            return Ok(VisitDto);
        }


        [HttpPost("AddVisit")]
        public async Task<ActionResult<NewVisitResponse>> AddVisit(NewVisitRequest request)
        {

            //var userExists = await _context.Users.AnyAsync(u => u.Id == request.ClientId);

            //if (!userExists)
            //{
            //    return BadRequest(new { message = "User does not exist." });
            //}

            var VisitDb = new Visit();

            VisitDb.PropertyId = request.PropertyId;
            VisitDb.UserId = request.UserId;
            VisitDb.VisitDate = request.VisitDate;
            VisitDb.Status = request.Status;
            VisitDb.Notes = request.Notes;


            _context.Visits.Add(VisitDb);
            await _context.SaveChangesAsync();

            return Ok(new NewVisitResponse { Id = VisitDb.Id });
        }

        //var response = new NewVisitResponse { Id = VisitDb.Id };
        //return response;
        //return VisitDb.Id;


        [HttpPut("UpdateVisit/{id}")]
        public async Task<ActionResult<NewVisitResponse>> UpdateVisit(int Id, NewVisitRequest request)
        {
            var VisitDb = await _context.Visits.FindAsync(Id);

            if (VisitDb == null)
            {
                return NotFound();
            }

            VisitDb.PropertyId = request.PropertyId;
            VisitDb.UserId = request.UserId;
            VisitDb.VisitDate = request.VisitDate;
            VisitDb.Status = request.Status;
            VisitDb.Notes = request.Notes;
            

            _context.Visits.Update(VisitDb);
            await _context.SaveChangesAsync();

            return Ok(new NewVisitResponse { Id = VisitDb.Id });

        }

        [HttpDelete("DeleteVisit/{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var VisitDb = await _context.Visits.FindAsync(id);
            if (VisitDb == null)
            {
                return NotFound();
            }

            _context.Visits.Remove(VisitDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
