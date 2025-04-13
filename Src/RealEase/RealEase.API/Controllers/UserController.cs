using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.API.Dtos;
using RealEase.API.Requests;
using RealEase.API.Responses;
using RealEase.Infrastructure.Interfaces;
using RealEase.Persistence.Context;
using System.Net;
using static RealEase.API.Requests.NewUserRequest;
using User = RealEase.Domain.Entities.User;



namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }


        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound(new { message = "Usuario no encontrado." });

            var userResponse = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.Role,
                user.IsActive
            };

            return Ok(userResponse);
        }




        [HttpPost("AddUser")]
        public async Task<ActionResult<NewUserResponse>> AddUser(NewUserRequest request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = request.PasswordHash,
                Role = request.Role,
                IsActive = request.IsActive
            };

            var newUser = await _userRepository.CreateAsync(user);

            return Ok(new NewUserResponse { Id = newUser.Id });
        }



        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<NewUserResponse>> UpdateUser(int id, NewUserRequest request)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser válido.");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound("Usuario no encontrado.");

            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.Email = request.Email;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.PasswordHash = request.PasswordHash;
            existingUser.Role = request.Role;
            existingUser.IsActive = request.IsActive;

            var updatedUser = await _userRepository.UpdateAsync(existingUser);

            return Ok(new NewUserResponse { Id = updatedUser.Id });
        }





        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return NotFound("Usuario no encontrado.");

            var deleted = await _userRepository.DeleteAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el usuario.");

            return Ok(new { message = "Usuario eliminado exitosamente." });
        }


    }
}