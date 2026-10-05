namespace Viamatica.Domain.Entities;

public class UserStatus
{
    public string StatusId { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}