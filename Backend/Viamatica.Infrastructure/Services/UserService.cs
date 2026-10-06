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
}