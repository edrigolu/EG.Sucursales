namespace EG.Sucursales.Domain.Entities
{
    public class Moneda
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
        public bool Estado { get; set; }

    }
}
