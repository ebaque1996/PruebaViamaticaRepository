namespace Viamatica.Domain.Entities;

public class Menu
{
    public int MenuId { get; set; }
    public string Title { get; set; } = null!;
    public string Route { get; set; } = null!;
    public string? Icon { get; set; }
    public int Order { get; set; }

    public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}