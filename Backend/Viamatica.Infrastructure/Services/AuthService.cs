using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Viamatica.Application.DTOs;
using Viamatica.Application.Interfaces;
using Viamatica.Domain.Entities;
using Viamatica.Infrastructure.Persistence;

namespace Viamatica.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ViamaticaDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ViamaticaDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        // 1. Buscar usuario por Username o por Email codificado en Base64
        var encodedInput = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.EmailOrUsername));

        var user = await _context.Users
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => (u.Username == request.EmailOrUsername || u.Email == encodedInput) && !u.IsDeleted);

        if (user == null)
            throw new Exception("Credenciales inválidas.");

        // 2. Verificar estado activo
        if (user.UserStatusStatusId != "ACT")
            throw new Exception("El usuario no se encuentra activo o está pendiente de aprobación.");

        // 3. Verificar contraseña con BCrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.PasswordHash, user.Password);
        if (!isPasswordValid)
            throw new Exception("Credenciales inválidas.");

        // 4. Mapear datos del usuario
        var roleName = user.Rol?.RolName ?? "Usuario";
        var decodedEmail = DecodeBase64(user.Email);

        var userDto = new UserAuthDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = decodedEmail,
            FullName = user.Username,
            RoleName = roleName
        };

        // 5. Generar JWT Token
        var token = GenerateJwtToken(user, roleName, decodedEmail);

        // 6. Obtener el menú dinámico según el Rol
        var menu = GetMenuByRole(roleName);

        return new LoginResponseDto
        {
            Token = token,
            User = userDto,
            Menu = menu
        };
    }

    private string GenerateJwtToken(User user, string roleName, string email)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var secretKey = jwtSettings["Key"] ?? "ClaveSecretaSuperSeguraParaViamaticaSystem2026!";
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, roleName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"] ?? "ViamaticaApi",
            Audience = jwtSettings["Audience"] ?? "ViamaticaFrontend"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private List<MenuItemDto> GetMenuByRole(string roleName)
    {
        var menu = new List<MenuItemDto>
        {
            new MenuItemDto { Id = 1, Title = "Bienvenida", Route = "/welcome", Icon = "home" }
        };

        if (roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase) || roleName.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
        {
            menu.Add(new MenuItemDto { Id = 2, Title = "Dashboard Admin", Route = "/admin/dashboard", Icon = "dashboard" });
            menu.Add(new MenuItemDto { Id = 3, Title = "Gestión de Usuarios", Route = "/admin/users", Icon = "people" });
        }
        else if (roleName.Equals("Gestor", StringComparison.OrdinalIgnoreCase))
        {
            menu.Add(new MenuItemDto { Id = 4, Title = "Asignación de Turnos", Route = "/gestor/shift-assignment", Icon = "assignment" });
        }
        else if (roleName.Equals("Cajero", StringComparison.OrdinalIgnoreCase))
        {
            menu.Add(new MenuItemDto { Id = 5, Title = "Mantenimiento Clientes", Route = "/cajero/clients", Icon = "person" });
            menu.Add(new MenuItemDto { Id = 6, Title = "Procesos de Caja", Route = "/cajero/cash-process", Icon = "point_of_sale" });
        }

        return menu;
    }

    public async Task<RecoverPasswordResponseDto> RecoverPasswordAsync(RecoverPasswordRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.EmailOrIdentification))
            throw new Exception("Debe ingresar un correo electrónico o cédula de ciudadanía.");

        // Codificamos la entrada a Base64 para comparar con Email e Identification almacenados
        var encodedInput = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.EmailOrIdentification.Trim()));

        var user = await _context.Users
            .FirstOrDefaultAsync(u => (u.Email == encodedInput || u.Identification == encodedInput || u.Username == request.EmailOrIdentification) 
                                    && !u.IsDeleted);

        if (user == null)
            throw new Exception("No se encontró ningún usuario registrado con la información ingresada.");

        if (user.UserStatusStatusId != "ACT")
            throw new Exception("El usuario no se encuentra activo en el sistema.");

        // Generar nueva contraseña (la enviada o una temporal por defecto)
        string newPassword = !string.IsNullOrWhiteSpace(request.NewPassword) 
            ? request.NewPassword 
            : $"Via{Random.Shared.Next(100000, 999999)}!";

        // Encriptar nueva contraseña con BCrypt
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new RecoverPasswordResponseDto
        {
            Message = "Se ha restablecido la contraseña correctamente.",
            TemporaryPassword = string.IsNullOrWhiteSpace(request.NewPassword) ? newPassword : null
        };
    }

    private string DecodeBase64(string base64EncodedData)
    {
        if (string.IsNullOrEmpty(base64EncodedData)) return string.Empty;
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}