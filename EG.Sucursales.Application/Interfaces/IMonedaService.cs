using EG.Sucursales.Application.DTOs;

namespace EG.Sucursales.Application.Interfaces
{
    public interface IMonedaService
    {
        Task<IEnumerable<MonedaDto>> ListarTodasAsync();
        Task<IEnumerable<MonedaDto>> ListarActivasAsync();
        Task<MonedaDto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(MonedaDto dto);
        Task<bool> ActualizarAsync(MonedaDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
