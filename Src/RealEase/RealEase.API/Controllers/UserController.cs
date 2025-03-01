using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain;
using RealEase.Domain.Entities;
using static RealEase.API.Requests.NewPropertieRequest;
using Propertie = RealEase.Domain.Entities.Propertie;


namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly RealEaseDbContext _context;

        public UserController(RealEaseDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetPropertie")]
        public async Task<ActionResult<List<Propertie>>> GetPropertie()
        {
            return await _context.Properties.ToListAsync();
        }

        [HttpPost("AddPropertie")]
        public async Task<ActionResult<NewPropertieResponse>> AddPropertie(NewPropertieRequest request)
        {
            var propertieDb = new Propertie();
            
            propertieDb.Title = request.Title;
            propertieDb.Description = request.Description;
            propertieDb.Address = request.Address;
            propertieDb.Price = request.Price;
       
            _context.Properties.Add(propertieDb);
            await _context.SaveChangesAsync();

            return new NewPropertieResponse { Id = propertieDb.Id };

            //var response = new NewPropertieResponse { Id = propertieDb.Id };
            //return response;
            //return propertieDb.Id;
        }
    }
}


