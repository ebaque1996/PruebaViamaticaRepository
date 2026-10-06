using System.ComponentModel.DataAnnotations;

namespace Viamatica.Application.DTOs;

public class CreateTurnDto
{
    [Required]
    public int CashCashId { get; set; }

    [Required]
    public int UserGestorId { get; set; }

    [Required]
    public string AttentionTypeId { get; set; } = null!;
}

public class TurnDto
{
    public int TurnId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
}