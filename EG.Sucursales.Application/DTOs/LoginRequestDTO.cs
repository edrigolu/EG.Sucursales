using System.ComponentModel.DataAnnotations;

namespace EG.Sucursales.Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string Correo { get; set; } = null!;

        [Required]
        public string Clave { get; set; } = null!;

        
    }
}
