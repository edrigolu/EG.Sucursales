using Dapper;
using EG.Sucursales.Domain.Entities;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using System.Data;

namespace EG.Sucursales.Infrastructure.Repositories
{
    public class MonedaRepository : GenericRepository<Moneda>, IMonedaRepository
    {
        public MonedaRepository(DapperContext context) : base(context, "mon") // <- Aquí va el prefijo correcto
        {
        }

        protected override DynamicParameters MapToParameters(Moneda moneda)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", moneda.Id);
            parameters.Add("@Nombre", moneda.Nombre);
            parameters.Add("@Codigo", moneda.Codigo);
            parameters.Add("@Simbolo", moneda.Simbolo);
            parameters.Add("@Estado", moneda.Estado);
            return parameters;
        }

        public async Task<IEnumerable<Moneda>> ListarActivasAsync()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Moneda>(
                "eg_mon_obtener_monedas_activas",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
