using Dapper;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using System.Data;

namespace EG.Sucursales.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DapperContext _context;
        private readonly string _prefix;
        private readonly string _entidad;

        public GenericRepository(DapperContext context, string prefix)
        {
            _context = context;
            _entidad = typeof(T).Name.ToLower(); // moneda, usuario, sucursal
            _prefix = prefix;                    // mon, seg, suc
        }

        public async Task<IEnumerable<T>> ListarAsync()
        {
            string sp = $"eg_{_prefix}_listar_{_entidad}s";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(sp,
                                                  commandType: CommandType.StoredProcedure);
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            string sp = $"eg_{_prefix}_obtener_{_entidad}_por_id";
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return await connection.QueryFirstOrDefaultAsync<T>(sp,
                                                                parameters,
                                                                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CrearAsync(T entidad)
        {
            string sp = $"eg_{_prefix}_crear_{_entidad}";
            using var connection = _context.CreateConnection();
            var parameters = MapToParameters(entidad);
            return await connection.ExecuteScalarAsync<int>(sp,
                                                            parameters,
                                                            commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ActualizarAsync(T entidad)
        {
            string sp = $"eg_{_prefix}_actualizar_{_entidad}";
            using var connection = _context.CreateConnection();
            var parameters = MapToParameters(entidad);
            var filas = await connection.ExecuteAsync(sp,
                                                      parameters,
                                                      commandType: CommandType.StoredProcedure);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            string sp = $"eg_{_prefix}_eliminar_{_entidad}";
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var filas = await connection.ExecuteAsync(sp,
                                                      parameters,
                                                      commandType: CommandType.StoredProcedure);
            return filas > 0;
        }

        protected abstract DynamicParameters MapToParameters(T entidad);
    }
}
