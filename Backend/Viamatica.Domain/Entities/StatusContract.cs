namespace Viamatica.Domain.Entities;

public class StatusContract
{
    public string StatusId { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}