namespace Viamatica.Domain.Entities;

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Identification { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ReferenceAddress { get; set; } = null!;

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Attention> Attentions { get; set; } = new List<Attention>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}