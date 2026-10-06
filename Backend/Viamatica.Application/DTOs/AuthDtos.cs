namespace Viamatica.Application.DTOs;

public class LoginRequestDto
{
    public string EmailOrUsername { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}

public class UserAuthDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}

public class MenuItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Route { get; set; } = null!;
    public string? Icon { get; set; }
}

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public UserAuthDto User { get; set; } = null!;
    public List<MenuItemDto> Menu { get; set; } = new();
}