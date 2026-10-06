using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;
public interface IUserService
{
    Task<string> CreateUserAsync(CreateUserDto dto, string creatorRole);
}
