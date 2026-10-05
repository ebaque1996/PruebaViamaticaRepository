namespace Viamatica.Domain.Entities;

public class AttentionStatus
{
    public int StatusId { get; set; }
    public string Description { get; set; } = null!;

    public ICollection<Attention> Attentions { get; set; } = new List<Attention>();
}