using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;

namespace Viamatica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Descomenta esto si usas JWT u otra autenticación
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        try
        {
            // Opción 1: Obtener el rol del usuario autenticado (desde el token)
            // var creatorRole = User.FindFirstValue(ClaimTypes.Role) ?? "SinRol";

            // Opción 2: Si el rol viene en un header personalizado para pruebas
            //var creatorRole = Request.Headers["X-User-Role"].ToString();
            var creatorRole = "Administrador"; // Simulación para pruebas, reemplaza con la lógica real

            // Validamos que el DTO sea correcto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Llamamos al servicio con los parámetros de la interfaz
            string result = await _userService.CreateUserAsync(dto, creatorRole);

            // Puedes devolver un 200 OK o un 201 Created
            return Ok(new { message = "Usuario creado exitosamente", id = result });
        }
        catch (UnauthorizedAccessException ex) // Si el servicio lanza error por permisos
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            // Manejo genérico de errores (idealmente usarías un middleware global)
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _userService.DeleteAsync(id);
            
            if (!result)
                return NotFound(new { message = "Usuario no encontrado o ya fue eliminado." });

            return Ok(new { message = "Usuario eliminado correctamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
