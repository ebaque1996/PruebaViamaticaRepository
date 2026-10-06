using Viamatica.Application.DTOs;
using Viamatica.Domain.Entities;

namespace Viamatica.Application.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client> GetByIdAsync(int id);
    Task<Client> CreateAsync(CreateClientDto dto);
    Task<bool> DeleteAsync(int id);
}