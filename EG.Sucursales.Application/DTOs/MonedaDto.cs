using System.ComponentModel.DataAnnotations;

namespace EG.Sucursales.Application.DTOs
{
    public class MonedaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(10, ErrorMessage = "El código no puede exceder los 10 caracteres")]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = "El símbolo es obligatorio")]
        [StringLength(5, ErrorMessage = "El símbolo no puede exceder los 5 caracteres")]
        public string Simbolo { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
