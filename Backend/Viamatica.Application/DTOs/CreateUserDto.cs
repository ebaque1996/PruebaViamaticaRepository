using System.ComponentModel.DataAnnotations;

namespace Viamatica.Application.DTOs;

public class CreateUserDto
{
    // 8 a 20 caracteres, letras y al menos un número, SIN caracteres especiales
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", 
        ErrorMessage = "El usuario debe tener entre 8 y 20 caracteres, contener letras, al menos un número y ningún carácter especial.")]
    public string Username { get; set; } = null!;

    // Mínimo 8, máximo 30, al menos un número y una mayúscula
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,30}$", 
        ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres, al menos una letra mayúscula y al menos un número.")]
    public string Password { get; set; } = null!;

    [Required]
    public string Identification { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string Email { get; set; } = null!;

    [Required]
    public int RolId { get; set; } 
}