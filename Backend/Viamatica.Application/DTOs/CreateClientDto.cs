using System.ComponentModel.DataAnnotations;

namespace Viamatica.Application.DTOs;

public class CreateClientDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string Email { get; set; } = null!;

    // Identificación: Mínimo 10, máximo 13, solo números
    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [RegularExpression(@"^\d{10,13}$", 
        ErrorMessage = "La identificación debe tener entre 10 y 13 dígitos y contener solo números.")]
    public string Identification { get; set; } = null!;

    // Teléfono: Empieza con 09, solo números, mínimo 10 dígitos (^09 seguido de al menos 8 dígitos)
    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    [RegularExpression(@"^09\d{8,}$", 
        ErrorMessage = "El número de teléfono debe empezar con 09, contener solo números y tener al menos 10 dígitos.")]
    public string PhoneNumber { get; set; } = null!;

    // Dirección: Entre 20 y 100 caracteres
    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(100, MinimumLength = 20, 
        ErrorMessage = "La dirección debe tener al menos 20 caracteres y un máximo de 100.")]
    public string Address { get; set; } = null!;

    // Referencia: Entre 20 y 100 caracteres
    [Required(ErrorMessage = "La referencia de la dirección es obligatoria.")]
    [StringLength(100, MinimumLength = 20, 
        ErrorMessage = "La referencia de la dirección debe tener al menos 20 caracteres y un máximo de 100.")]
    public string ReferenceAddress { get; set; } = null!;
}