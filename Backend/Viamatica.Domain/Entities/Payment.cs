namespace Viamatica.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public int ClientClientId { get; set; }
    public Client Client { get; set; } = null!;
}