using MyWallet.Application.DTOs;

namespace MyWallet.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto, Guid userId);
}
