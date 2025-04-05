using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Persistence.Context;
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

        [HttpGet(nameof(GetUsers))]
        public async Task<ActionResult<List<User>>> GetUsers()
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
                FirstName = UserDb.FirstName,
                LastName = UserDb.LastName,
                Email = UserDb.Email,
                PhoneNumber = UserDb.PhoneNumber,
                PasswordHash = UserDb.PasswordHash,
                Role = UserDb.Role,
                IsActive = UserDb.IsActive
            };

            return Ok(UserDto);
        }


        [HttpPost("AddUser")]
        public async Task<ActionResult<NewUserResponse>> AddUser(NewUserRequest request)
        {
            var UserDb = new User();
            
            UserDb.FirstName = request.FirstName;
            UserDb.LastName = request.LastName;
            UserDb.Email = request.Email;
            UserDb.PhoneNumber = request.PhoneNumber;
            UserDb.PasswordHash = request.PasswordHash;
            UserDb.Role = request.Role;
            UserDb.IsActive = request.IsActive;

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

            UserDb.FirstName = request.FirstName;
            UserDb.LastName = request.LastName;
            UserDb.Email = request.Email;
            UserDb.PhoneNumber = request.PhoneNumber;
            UserDb.PasswordHash = request.PasswordHash;
            UserDb.Role = request.Role;
            UserDb.IsActive = request.IsActive;

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


