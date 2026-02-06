using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface IAccountService
{
    Task<AccountResponseDto> CreateAsync(CreateAccountDto dto, Guid userId);
    Task<List<AccountResponseDto>> GetAllAsync(Guid userId);
    Task<AccountResponseDto> GetByIdAsync(Guid id, Guid userId);
    Task<AccountResponseDto> UpdateAsync(Guid id, UpdateAccountDto dto, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);

}
