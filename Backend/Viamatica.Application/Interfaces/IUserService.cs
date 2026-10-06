using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;
public interface IUserService
{
    Task<string> CreateUserAsync(CreateUserDto dto, string creatorRole);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}
