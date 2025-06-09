using EG.Sucursales.Application.DTOs;

namespace EG.Sucursales.Application.Interfaces
{
    public interface ISucursalService
    {
        Task<IEnumerable<SucursalDto>> ListarAsync();
        Task<SucursalDto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(SucursalDto dto);
        Task<bool> ActualizarAsync(SucursalDto dto);
        Task<bool> EliminarAsync(int id);
        Task<bool> ExisteCodigoAsync(int codigo);
    }
}
