namespace Viamatica.Domain.Entities;

public class MethodPayment
{
    public int MethodPaymentId { get; set; }
    public string Description { get; set; } = null!;

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}