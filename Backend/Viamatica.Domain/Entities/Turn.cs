using System.ComponentModel.DataAnnotations;

namespace Viamatica.Domain.Entities;

public class Turn
{
    public int TurnId { get; set; }
    
    [RegularExpression(@"^[A-Z]{2}\d{4}$")] 
    [StringLength(6)]
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int CashCashId { get; set; }
    public Cash Cash { get; set; } = null!;

    public int UserGestorId { get; set; }

    public string AttentionTypeId { get; set; } = null!;
    public AttentionType AttentionType { get; set; } = null!;
    
    public int? AttentionId { get; set; }
    public Attention? Attention { get; set; }

    // Eliminación lógica
    public bool IsDeleted { get; set; } = false;
}