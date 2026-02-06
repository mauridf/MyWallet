using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto, Guid userId);
    Task<IEnumerable<TransactionResponseDto>> GetFilteredAsync(
        Guid userId,
        int? year,
        int? month,
        Guid? accountId
    );
}
