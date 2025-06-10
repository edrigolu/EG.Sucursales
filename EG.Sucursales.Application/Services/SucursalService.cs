using EG.Sucursales.Application.DTOs;
using EG.Sucursales.Application.Interfaces;
using EG.Sucursales.Domain.Entities;
using EG.Sucursales.Domain.Interfaces;

namespace EG.Sucursales.Application.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _sucursalRepository;

        public SucursalService(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<IEnumerable<SucursalDto>> ListarAsync()
        {
            var sucursales = await _sucursalRepository.ListarAsync();
            return sucursales.Select(MapToDto);
        }

        public async Task<SucursalDto?> ObtenerPorIdAsync(int id)
        {
            var sucursal = await _sucursalRepository.ObtenerPorIdAsync(id);
            return sucursal == null ? null : MapToDto(sucursal);
        }

        public async Task<int> CrearAsync(SucursalDto dto)
        {
            var entidad = MapToEntity(dto);
            return await _sucursalRepository.CrearAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(SucursalDto dto)
        {
            var entidad = MapToEntity(dto);
            return await _sucursalRepository.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _sucursalRepository.EliminarAsync(id);
        }

        public async Task<bool> ExisteCodigoAsync(int codigo)
        {
            return await _sucursalRepository.ExisteCodigoAsync(codigo);
        }

        // Mapeos
        private SucursalDto MapToDto(Sucursal entidad)
        {
            return new SucursalDto
            {
                Id = entidad.Id,
                Codigo = int.Parse(entidad.Codigo),
                Descripcion = entidad.Descripcion,
                Direccion = entidad.Direccion,
                Identificacion = entidad.Identificacion,
                FechaCreacion = entidad.FechaCreacion,
                IdMoneda = entidad.IdMoneda
            };
        }

        private Sucursal MapToEntity(SucursalDto dto)
        {
            return new Sucursal
            {
                Id = dto.Id,
                Codigo = dto.Codigo.ToString(),
                Descripcion = dto.Descripcion,
                Direccion = dto.Direccion,
                Identificacion = dto.Identificacion,
                FechaCreacion = dto.FechaCreacion,
                IdMoneda = dto.IdMoneda
            };
        }
    }
}
