using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> CreateAsync(CreateUserDto dto);
    Task<UserResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
}
