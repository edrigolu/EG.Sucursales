using System.ComponentModel.DataAnnotations;

namespace EG.Sucursales.Application.DTOs
{
    public class SucursalDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El código debe ser mayor que cero")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Identificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una moneda válida")]
        public int IdMoneda { get; set; }
    }
}
