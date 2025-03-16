using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Domain;
using RealEase.Domain.Entities;
using System.Net;
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

        [HttpGet("GetUser/{Id}")]
        public async Task<ActionResult<UserDto>> GetUser(int Id)
        {
            var UserDb = await _context.Users.FindAsync(Id);

            if (UserDb == null)
            {
                return NotFound();
            }

            var UserDto = new UserDto
            {
                Id = UserDb.Id,
                Name = UserDb.Name,
                Lastname = UserDb.Lastname,
                IsActive = UserDb.IsActive,
                Address = UserDb.Address
            };

            return Ok(UserDto);
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

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<NewUserResponse>> UpdateUser(int Id, NewUserRequest request)
        {
            var UserDb = await _context.Users.FindAsync(Id);

            if (UserDb == null)
            {
                return NotFound();
            }

            UserDb.Name = request.Name;
            UserDb.Lastname = request.Lastname;
            UserDb.IsActive = request.IsActive;
            UserDb.Address = request.Address;

            _context.Users.Update(UserDb);
            await _context.SaveChangesAsync();

            return Ok(new NewUserResponse { Id = UserDb.Id });

        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var UserDb = await _context.Users.FindAsync(id);
            if (UserDb == null)
            {
                return NotFound();
            }

            _context.Users.Remove(UserDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


