namespace Viamatica.Domain.Entities;

public class AttentionType
{
    public string AttentionTypeId { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<Attention> Attentions { get; set; } = new List<Attention>();
}