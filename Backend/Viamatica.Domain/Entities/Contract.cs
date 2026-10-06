namespace Viamatica.Domain.Entities;

public class Contract
{
    public int ContractId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int ServiceServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public string StatusContractStatusId { get; set; } = null!;
    public StatusContract StatusContract { get; set; } = null!;

    public int ClientClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int MethodPaymentMethodPaymentId { get; set; }
    public MethodPayment MethodPayment { get; set; } = null!;

    // Eliminación lógica
    public bool IsDeleted { get; set; } = false;
}