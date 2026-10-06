using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;
public interface ITurnService
{
    Task<TurnDto> CreateAsync(CreateTurnDto dto);
    Task<TurnDto> GetByIdAsync(int id);
    Task<IEnumerable<TurnDto>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}
