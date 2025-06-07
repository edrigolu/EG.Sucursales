namespace EG.Sucursales.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int IdRol { get; set; }
        public bool Estado { get; set; }

        public Rol? Rol { get; set; }
    }
}
