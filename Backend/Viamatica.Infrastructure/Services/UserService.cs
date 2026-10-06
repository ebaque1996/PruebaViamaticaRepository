using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ViamaticaDbContext _context;

    public UserService(ViamaticaDbContext context)
    {
        _context = context;
    }

    // Nota: creatorRole simula el rol de quien está logueado haciendo la petición. 
    // Más adelante esto lo sacaremos del JWT automáticamente.
    public async Task<string> CreateUserAsync(CreateUserDto dto, string creatorRole)
    {
        // 1. Validar duplicados (Username no debe estar duplicado)
        var exists = await _context.Users.AnyAsync(u => u.Username == dto.Username && !u.IsDeleted);
        if (exists)
            throw new Exception("El nombre de usuario ya existe.");

        // 2. Regla de negocio: Administrador aprueba Gestores/Cajeros. Gestor los crea "Pendientes"
        string statusId;
        if (creatorRole == "Administrador")
        {
            statusId = "ACT"; // Activo directamente
        }
        else if (creatorRole == "Gestor")
        {
            statusId = "PEN"; // Pendiente de aprobación
        }
        else
        {
            throw new Exception("No tienes permisos para crear usuarios.");
        }

        // 3. Encriptación (Simulando encriptación básica 64 para ID y Correo por rapidez, BCrypt para pass)
        // En un entorno real se usa AES-256 para ID y Email.
        string encryptedEmail = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(dto.Email));
        string encryptedId = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(dto.Identification));
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Username = dto.Username,
            Password = hashedPassword,
            Identification = encryptedId,
            Email = encryptedEmail,
            RolRolId = dto.RolId,
            UserStatusStatusId = statusId,
            IsDeleted = false
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return statusId == "PEN" 
            ? "Usuario creado exitosamente. Pendiente de aprobación por un Administrador." 
            : "Usuario creado y activado exitosamente.";
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        // 1. Traemos de la BD solo los que no están eliminados
        var users = await _context.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();

        // 2. Mapeamos y desencriptamos en memoria
        return users.Select(u => new UserDto
        {
            UserId = u.UserId,
            Username = u.Username,
            Email = DecodeBase64(u.Email),
            Identification = DecodeBase64(u.Identification),
            StatusId = u.UserStatusStatusId,
            RolId = u.RolRolId,
            CreationDate = u.CreationDate
        });
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var u = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id && !u.IsDeleted);

        if (u == null)
            throw new Exception($"El usuario con ID {id} no fue encontrado o fue eliminado.");

        return new UserDto
        {
            UserId = u.UserId,
            Username = u.Username,
            Email = DecodeBase64(u.Email),
            Identification = DecodeBase64(u.Identification),
            StatusId = u.UserStatusStatusId,
            RolId = u.RolRolId,
            CreationDate = u.CreationDate
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null || user.IsDeleted)
            return false;

        // Eliminación lógica: Solo cambiamos la bandera
        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        
        return true;
    }

    // Método privado auxiliar para desencriptar el Base64
    private string DecodeBase64(string base64EncodedData)
    {
        if (string.IsNullOrEmpty(base64EncodedData)) return string.Empty;
        
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}