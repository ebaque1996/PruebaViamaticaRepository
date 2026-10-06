namespace Viamatica.Application.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Identification { get; set; } = null!;
    public string StatusId { get; set; } = null!;
    public int RolId { get; set; }
    public DateTime CreationDate { get; set; }
}