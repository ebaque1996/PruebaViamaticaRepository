using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;

public interface IRolService
{
    Task<IEnumerable<RolDto>> GetAllAsync();
    Task<RolDto> CreateAsync(CreateRolDto dto);
}