namespace Viamatica.Domain.Entities;

public class UserCash
{
    public int UserUserId { get; set; }
    public User User { get; set; } = null!;

    public int CashCashId { get; set; }
    public Cash Cash { get; set; } = null!;
}