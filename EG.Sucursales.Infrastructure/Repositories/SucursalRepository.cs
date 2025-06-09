using Dapper;
using EG.Sucursales.Domain.Entities;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using System.Data;

namespace EG.Sucursales.Infrastructure.Repositories
{
    public class SucursalRepository : GenericRepository<Sucursal>, ISucursalRepository
    {
        public SucursalRepository(DapperContext context) : base(context, "suc") { }

        protected override DynamicParameters MapToParameters(Sucursal entidad)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entidad.Id);
            parameters.Add("@Codigo", entidad.Codigo); // tipo int
            parameters.Add("@Descripcion", entidad.Descripcion);
            parameters.Add("@Direccion", entidad.Direccion);
            parameters.Add("@Identificacion", entidad.Identificacion);
            parameters.Add("@FechaCreacion", entidad.FechaCreacion);
            parameters.Add("@IdMoneda", entidad.IdMoneda);
            return parameters;
        }

        public async Task<bool> ExisteCodigoAsync(int codigo)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Codigo", codigo);

            var resultado = await connection.ExecuteScalarAsync<int>(
                "eg_suc_existe_codigo",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado > 0;
        }
    }
}
