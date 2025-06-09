namespace EG.Sucursales.Domain.Entities
{
    public class Moneda
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
