using Microsoft.AspNetCore.Mvc;
using Viamatica.Application.Interfaces;
using Viamatica.Application.DTOs.Contracts;

namespace Viamatica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost("change-service")]
    public async Task<IActionResult> ChangeService([FromBody] ChangeServiceRequestDto request)
    {
        // 1. Validación básica
        if (request == null || request.OldContractId <= 0 || request.NewServiceId <= 0)
        {
            return BadRequest(new { message = "Los datos enviados son inválidos. Se requiere el contrato anterior y el nuevo servicio." });
        }

        try
        {
            // 2. Llamada a la capa de servicio (que ejecuta el Stored Procedure)
            await _contractService.ChangeOrRenewContractAsync(request.OldContractId, request.NewServiceId);
            
            // 3. Respuesta exitosa
            return Ok(new { message = "El contrato ha sido sustituido y renovado con éxito." });
        }
        catch (Exception ex)
        {
            // Si el Stored Procedure lanza el RAISERROR ('El contrato anterior no existe.'), 
            // caerá en este catch.
            return StatusCode(500, new 
            { 
                message = "Ocurrió un error al procesar la renovación del contrato.", 
                error = ex.Message 
            });
        }
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        try
        {
            var success = await _contractService.CancelContractAsync(id);

            if (!success)
                return NotFound(new { message = $"No se encontró un contrato activo con el ID {id}." });

            return Ok(new { message = "El contrato ha sido cancelado exitosamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno al cancelar el contrato.", error = ex.Message });
        }
    }
}