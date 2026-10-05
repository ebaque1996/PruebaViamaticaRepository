namespace Viamatica.Domain.Entities;

public class Rol
{
    public int RolId { get; set; }
    public string RolName { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}