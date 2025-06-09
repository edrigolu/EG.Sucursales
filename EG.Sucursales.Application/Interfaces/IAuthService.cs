using EG.Sucursales.Application.DTOs;

namespace EG.Sucursales.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequestDTO dto);
    }
}
