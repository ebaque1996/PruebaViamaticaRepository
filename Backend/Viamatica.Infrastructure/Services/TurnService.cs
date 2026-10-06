using Microsoft.EntityFrameworkCore;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class TurnService : ITurnService
{
    private readonly ViamaticaDbContext _context;

    public TurnService(ViamaticaDbContext context)
    {
        _context = context;
    }

    public async Task<TurnDto> CreateAsync(CreateTurnDto dto)
    {
        // Verificamos que el Tipo de Atención exista en la BD
        var attentionType = await _context.AttentionTypes
            .FirstOrDefaultAsync(at => at.AttentionTypeId == dto.AttentionTypeId.ToUpper());

        if (attentionType == null)
            throw new Exception($"El tipo de atención '{dto.AttentionTypeId}' no es válido o no existe.");

        // 2. Usamos el ID validado como prefijo (ej: "AC")
        string prefix = attentionType.AttentionTypeId;

        // 3. Buscar el último turno generado para este prefijo
        var lastTurn = await _context.Turns
            .Where(t => t.Description.StartsWith(prefix))
            .OrderByDescending(t => t.TurnId)
            .FirstOrDefaultAsync();

        // 4. Calcular el siguiente número
        int nextNumber = 1;
        if (lastTurn != null)
        {
            // Extraemos los últimos 4 dígitos. Usamos prefix.Length por si algún día hay prefijos de 3 letras
            string numberPart = lastTurn.Description.Substring(prefix.Length, 4);
            
            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        // 5. Formatear: AC + 0001 = AC0001
        string newDescription = $"{prefix}{nextNumber:D4}";

        // 6. Crear la entidad Turno asignando la llave foránea directa
        var turn = new Turn
        {
            Description = newDescription,
            Date = DateTime.UtcNow,
            CashCashId = dto.CashCashId,
            UserGestorId = dto.UserGestorId,
            AttentionTypeId = attentionType.AttentionTypeId
        };

        _context.Turns.Add(turn);
        await _context.SaveChangesAsync();

        return new TurnDto
        {
            TurnId = turn.TurnId,
            Description = turn.Description,
            Date = turn.Date
        };
    }

    public async Task<IEnumerable<TurnDto>> GetAllAsync()
    {
        return await _context.Turns
            .Where(t => !t.IsDeleted)
            .OrderByDescending(t => t.Date)
            .Select(t => new TurnDto
            {
                TurnId = t.TurnId,
                Description = t.Description,
                Date = t.Date
            })
            .ToListAsync();
    }

    public async Task<TurnDto> GetByIdAsync(int id)
    {
        var turn = await _context.Turns
            .Where(t => t.TurnId == id && !t.IsDeleted)
            .Select(t => new TurnDto
            {
                TurnId = t.TurnId,
                Description = t.Description,
                Date = t.Date
            })
            .FirstOrDefaultAsync();

        if (turn == null)
            throw new Exception($"No se encontró el turno con ID {id} o fue eliminado.");

        return turn;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var turn = await _context.Turns.FindAsync(id);

        if (turn == null || turn.IsDeleted)
            return false;

        turn.IsDeleted = true;
        
        await _context.SaveChangesAsync();
        
        return true;
    }
}