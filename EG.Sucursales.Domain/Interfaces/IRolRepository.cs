using EG.Sucursales.Domain.Entities;

namespace EG.Sucursales.Domain.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ListarAsync();
    }
}
