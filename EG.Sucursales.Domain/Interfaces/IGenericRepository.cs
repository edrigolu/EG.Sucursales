namespace EG.Sucursales.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListarAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(T entidad);
        Task<bool> ActualizarAsync(T entidad);
        Task<bool> EliminarAsync(int id);
    }
}
