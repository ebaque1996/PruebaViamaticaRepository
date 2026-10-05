namespace Viamatica.Domain.Entities;

public class Attention
{
    public int AttentionId { get; set; }

    public int TurnTurnId { get; set; }
    public Turn Turn { get; set; } = null!;

    public int ClientClientId { get; set; }
    public Client Client { get; set; } = null!;

    public string AttentionTypeAttentionTypeId { get; set; } = null!;
    public AttentionType AttentionType { get; set; } = null!;

    public int AttentionStatusStatusId { get; set; }
    public AttentionStatus AttentionStatus { get; set; } = null!;
}