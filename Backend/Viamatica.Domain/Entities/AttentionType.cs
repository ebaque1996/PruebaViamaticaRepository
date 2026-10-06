namespace Viamatica.Domain.Entities;

public class AttentionType
{
    public string AttentionTypeId { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<Turn> Turns { get; set; } = new List<Turn>();
}