using Microsoft.EntityFrameworkCore;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class RolService : IRolService
{
    private readonly ViamaticaDbContext _context;

    public RolService(ViamaticaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RolDto>> GetAllAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        
        // Mapeamos de Entidad a DTO manualmente (luego podemos usar AutoMapper si prefieres)
        return roles.Select(r => new RolDto 
        { 
            RolId = r.RolId, 
            RolName = r.RolName 
        });
    }

    public async Task<RolDto> CreateAsync(CreateRolDto dto)
    {
        var nuevoRol = new Rol
        {
            RolName = dto.RolName
        };

        _context.Roles.Add(nuevoRol);
        await _context.SaveChangesAsync();

        return new RolDto
        {
            RolId = nuevoRol.RolId,
            RolName = nuevoRol.RolName
        };
    }
}