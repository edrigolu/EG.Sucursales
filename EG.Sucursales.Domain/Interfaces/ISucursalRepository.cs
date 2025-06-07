using EG.Sucursales.Domain.Entities;

namespace EG.Sucursales.Domain.Interfaces
{
    public interface ISucursalRepository : IGenericRepository<Sucursal>
    {
        Task<bool> ExisteCodigoAsync(int codigo);
    }
}
