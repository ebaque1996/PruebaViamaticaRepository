using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<RecoverPasswordResponseDto> RecoverPasswordAsync(RecoverPasswordRequestDto request);
}