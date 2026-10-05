namespace Viamatica.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public int? UserCreate { get; set; }
    public int? UserApproval { get; set; }
    public DateTime? DateApproval { get; set; }

    public string UserStatusStatusId { get; set; } = null!;
    public UserStatus UserStatus { get; set; } = null!;

    public int RolRolId { get; set; }
    public Rol Rol { get; set; } = null!;

    public ICollection<UserCash> UserCashes { get; set; } = new List<UserCash>();
}