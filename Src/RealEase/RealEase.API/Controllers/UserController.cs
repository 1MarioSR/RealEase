using Microsoft.AspNetCore.Mvc;
using RealEase.Application.Dtos.User;
using RealEase.Application.Services;
using RealEase.Domain.Entities;

namespace RealEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync(string.Empty);
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "Usuario no encontrado." });

            var userResponse = new
            {
                user.Id,
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
        public async Task<IActionResult> AddUser(UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = await _userService.AddUserAsync(request);
            if (userId == 0)
                return StatusCode(500, "No se pudo crear el usuario.");

            return Ok(new { Id = userId });
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto request)
        {
            if (id <= 0) return BadRequest("El ID debe ser válido.");
            if (id != request.Id) return BadRequest("El ID de ruta y el ID del usuario no coinciden.");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound("Usuario no encontrado.");

            var updated = await _userService.UpdateUserAsync(request);
            if (!updated) return StatusCode(500, "No se pudo actualizar el usuario.");

            return Ok(new { Id = id, message = "Usuario actualizado exitosamente." });
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser un número válido.");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound("Usuario no encontrado.");

            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return StatusCode(500, "No se pudo eliminar el usuario.");

            return Ok(new { message = "Usuario eliminado exitosamente." });
        }
    }
}