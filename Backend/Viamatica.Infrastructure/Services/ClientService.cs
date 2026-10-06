using Microsoft.EntityFrameworkCore;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly ViamaticaDbContext _context;

    public ClientService(ViamaticaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        // Solo devolvemos los que no están eliminados
        return await _context.Clients
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.ClientId == id && !c.IsDeleted);
            
        if (client == null)
            throw new Exception("Cliente no encontrado.");
            
        return client;
    }

    public async Task<Client> CreateAsync(CreateClientDto dto)
    {
        // Validación de negocio: No repetir identificación
        var exists = await _context.Clients.AnyAsync(c => c.Identification == dto.Identification);
        if (exists)
            throw new Exception("Ya existe un cliente registrado con esta identificación.");

        var client = new Client
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Identification = dto.Identification,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            ReferenceAddress = dto.ReferenceAddress,
            IsDeleted = false
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null || client.IsDeleted)
            return false;

        // Eliminación lógica
        client.IsDeleted = true;
        await _context.SaveChangesAsync();
        
        return true;
    }
}