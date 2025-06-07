using EG.Sucursales.Domain.Entities;

namespace EG.Sucursales.Domain.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> LoginAsync(string correo, string clave);
    }
}
