using Microsoft.AspNetCore.Mvc;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;

namespace Viamatica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurnController : ControllerBase
{
    private readonly ITurnService _turnService;

    public TurnController(ITurnService turnService)
    {
        _turnService = turnService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turns = await _turnService.GetAllAsync();
        return Ok(turns);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var turn = await _turnService.GetByIdAsync(id);
            return Ok(turn);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTurnDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var turn = await _turnService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = turn.TurnId }, turn);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _turnService.DeleteAsync(id);
        
        if (!result)
            return NotFound(new { message = "Turno no encontrado o ya fue eliminado." });

        return Ok(new { message = "Turno eliminado correctamente." });
    }
}