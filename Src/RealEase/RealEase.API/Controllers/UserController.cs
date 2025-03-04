using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain;
using RealEase.Domain.Entities;
using static RealEase.API.Requests.NewUserRequest;
using User = RealEase.Domain.Entities.User;


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

        [HttpGet(nameof(GetUser))]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<NewUserResponse>> AddUser(NewUserRequest request)
        {
            var UserDb = new User();
            
            UserDb.Name = request.Name;
            UserDb.Lastname = request.Lastname;
            UserDb.IsActive = request.IsActive;
            UserDb.Address = request.Address;
       
            _context.Users.Add(UserDb);
            await _context.SaveChangesAsync();

            return new NewUserResponse { Id = UserDb.Id };

            //var response = new NewUserResponse { Id = UserDb.Id };
            //return response;
            //return UserDb.Id;
        }
    }
}


