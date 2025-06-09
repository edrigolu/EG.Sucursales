using Dapper;
using EG.Sucursales.Domain.Entities;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using System.Data;

namespace EG.Sucursales.Infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DapperContext context) : base(context, "seg") { }

        public async Task<Usuario?> LoginAsync(string correo, string clave)
        {
            string sp = "eg_seg_obtener_usuario_por_correo";
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Correo", correo);

            var resultado = await connection.QueryAsync<Usuario, Rol, Usuario>(
                sp,
                (user, rol) =>
                {
                    user.Rol = rol;
                    return user;
                },
                parameters,
                splitOn: "IdRol",
                commandType: CommandType.StoredProcedure
            );

            var usuario = resultado.FirstOrDefault();

            if (usuario is null || usuario.Clave != clave)
                return null;

            return usuario;
        }

        protected override DynamicParameters MapToParameters(Usuario entidad)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entidad.Id);
            parameters.Add("@NombreCompleto", entidad.NombreCompleto);
            parameters.Add("@Correo", entidad.Correo);
            parameters.Add("@Clave", entidad.Clave);
            parameters.Add("@IdRol", entidad.IdRol);
            parameters.Add("@Estado", entidad.Estado);
            return parameters;
        }
    }
}
