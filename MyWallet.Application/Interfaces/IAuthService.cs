using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}
