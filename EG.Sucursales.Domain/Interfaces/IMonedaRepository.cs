using EG.Sucursales.Domain.Entities;

namespace EG.Sucursales.Domain.Interfaces
{
    public interface IMonedaRepository : IGenericRepository<Moneda>
    {
        Task<IEnumerable<Moneda>> ListarActivasAsync();  // eg_mon_obtener_monedas_activas
    }
}
