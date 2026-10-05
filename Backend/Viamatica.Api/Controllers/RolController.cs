using Microsoft.AspNetCore.Mvc;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;

namespace Viamatica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    private readonly IRolService _rolService;

    public RolController(IRolService rolService)
    {
        _rolService = rolService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _rolService.GetAllAsync();
        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var rol = await _rolService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = rol.RolId }, rol);
    }
}