namespace Viamatica.Domain.Entities;

public class Turn
{
    public int TurnId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int CashCashId { get; set; }
    public Cash Cash { get; set; } = null!;

    public int UserGestorId { get; set; }
    public Attention? Attention { get; set; }
}