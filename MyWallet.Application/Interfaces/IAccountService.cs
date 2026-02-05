using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface IAccountService
{
    Task<AccountResponseDto> CreateAsync(CreateAccountDto dto, Guid userId);
    Task<List<AccountResponseDto>> GetAllAsync(Guid userId);
}
