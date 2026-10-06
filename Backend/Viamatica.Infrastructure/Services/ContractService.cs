using Microsoft.EntityFrameworkCore;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class ContractService : IContractService
{
    private readonly ViamaticaDbContext _context;

    public ContractService(ViamaticaDbContext context)
    {
        _context = context;
    }

    public async Task ChangeOrRenewContractAsync(int oldContractId, int newServiceId)
    {
        // Ejecutamos el Stored Procedure pasando los IDs correctos
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_RenewOrChangeContract @p0, @p1", 
            oldContractId, 
            newServiceId
        );
    }

    public async Task<bool> CancelContractAsync(int contractId)
    {
        // 1. Buscamos el contrato activo que no esté eliminado
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.ContractId == contractId && !c.IsDeleted);

        if (contract == null)
            return false;

        // 2. Validar que no esté cancelado previamente
        if (contract.StatusContractStatusId == "CAN")
            throw new InvalidOperationException("El contrato ya se encuentra cancelado.");

        // 3. Aplicar las reglas de negocio
        contract.StatusContractStatusId = "CAN";
        contract.EndDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}