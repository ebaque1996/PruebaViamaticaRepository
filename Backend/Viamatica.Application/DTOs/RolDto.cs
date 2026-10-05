namespace Viamatica.Application.DTOs;

public class RolDto
{
    public int RolId { get; set; }
    public string RolName { get; set; } = null!;
}

public class CreateRolDto
{
    public string RolName { get; set; } = null!;
}