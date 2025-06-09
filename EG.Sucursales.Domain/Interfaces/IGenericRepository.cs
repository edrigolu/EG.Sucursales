namespace EG.Sucursales.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListarAsync();          // SP: listar todos
        Task<T?> ObtenerPorIdAsync(int id);          // SP: obtener por id
        Task<int> CrearAsync(T entidad);             // SP: crear
        Task<bool> ActualizarAsync(T entidad);       // SP: actualizar
        Task<bool> EliminarAsync(int id);            // SP: eliminar
    }
}
