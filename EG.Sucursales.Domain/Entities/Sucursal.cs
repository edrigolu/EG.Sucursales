namespace EG.Sucursales.Domain.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int IdMoneda { get; set; }

        public Moneda? Moneda { get; set; }
    }
}
