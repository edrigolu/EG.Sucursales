namespace EG.Sucursales.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<string?> LoginAsync(string correo, string clave);
    }
}
