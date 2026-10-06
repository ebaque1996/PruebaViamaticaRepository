namespace Viamatica.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Identification { get; set; } = null!;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public int? UserCreate { get; set; }
    public int? UserApproval { get; set; }
    public DateTime? DateApproval { get; set; }

    public string UserStatusStatusId { get; set; } = null!;
    public UserStatus UserStatus { get; set; } = null!;

    public int RolRolId { get; set; }
    public Rol Rol { get; set; } = null!;
    
    // Eliminación lógica
    public bool IsDeleted { get; set; } = false;

    public ICollection<UserCash> UserCashes { get; set; } = new List<UserCash>();
}