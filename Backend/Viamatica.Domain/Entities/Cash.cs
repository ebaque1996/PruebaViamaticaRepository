namespace Viamatica.Domain.Entities;

public class Cash
{
    public int CashId { get; set; }
    public string CashDescription { get; set; } = null!;
    public string Active { get; set; } = "1";

    public ICollection<UserCash> UserCashes { get; set; } = new List<UserCash>();
    public ICollection<Turn> Turns { get; set; } = new List<Turn>();
}