using EG.Sucursales.Application.DTOs;
using EG.Sucursales.Application.Interfaces;
using EG.Sucursales.Domain.Entities;
using EG.Sucursales.Domain.Interfaces;

namespace EG.Sucursales.Application.Services
{
    public class MonedaService : IMonedaService
    {
        private readonly IMonedaRepository _monedaRepository;

        public MonedaService(IMonedaRepository monedaRepository)
        {
            _monedaRepository = monedaRepository;
        }

        public async Task<IEnumerable<MonedaDto>> ListarTodasAsync()
        {
            var monedas = await _monedaRepository.ListarAsync();
            return monedas.Select(MapToDto);
        }

        public async Task<IEnumerable<MonedaDto>> ListarActivasAsync()
        {
            var monedas = await _monedaRepository.ListarActivasAsync();
            return monedas.Select(MapToDto);
        }

        public async Task<MonedaDto?> ObtenerPorIdAsync(int id)
        {
            var moneda = await _monedaRepository.ObtenerPorIdAsync(id);
            return moneda == null ? null : MapToDto(moneda);
        }

        public async Task<int> CrearAsync(MonedaDto dto)
        {
            var entidad = MapToEntity(dto);
            return await _monedaRepository.CrearAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(MonedaDto dto)
        {
            var entidad = MapToEntity(dto);
            return await _monedaRepository.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _monedaRepository.EliminarAsync(id);
        }

        private static MonedaDto MapToDto(Moneda moneda)
        {
            return new()
            {
                Id = moneda.Id,
                Nombre = moneda.Nombre,
                Codigo = moneda.Codigo,
                Simbolo = moneda.Simbolo,
                Estado = moneda.Estado
            };
        }

        private static Moneda MapToEntity(MonedaDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Codigo = dto.Codigo,
                Simbolo = dto.Simbolo,
                Estado = dto.Estado
            };
        }
    }
}
