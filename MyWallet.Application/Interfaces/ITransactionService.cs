using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto, Guid userId);
    Task<List<TransactionResponseDto>> GetAllAsync(Guid userId);
    Task<List<TransactionResponseDto>> GetFilteredAsync(
        Guid userId,
        int? year,
        int? month,
        Guid? accountId
    );
}
