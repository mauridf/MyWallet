using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto, Guid userId)
    {
        var account = await _accountRepository.GetByIdAsync(dto.AccountId, userId);

        if (account == null)
            throw new InvalidOperationException("Account not found");

        if (dto.Type == TransactionType.Income)
            account.Credit(dto.Amount);
        else
            account.Debit(dto.Amount);

        var transaction = new Transaction(
            dto.Description,
            dto.Amount,
            dto.Type,
            dto.AccountId
        );

        await _transactionRepository.AddAsync(transaction);
        await _accountRepository.UpdateAsync(account);

        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type
        };
    }

    public async Task<List<TransactionResponseDto>> GetAllAsync(Guid userId)
    {
        var transactions = await _transactionRepository.GetAllByUserAsync(userId);

        return transactions.Select(t => new TransactionResponseDto
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            AccountId = t.AccountId,
            CreatedAt = t.CreatedAt
        }).ToList();
    }

    public async Task<List<TransactionResponseDto>> GetFilteredAsync(
        Guid userId,
        int? year,
        int? month,
        Guid? accountId
    )
    {
        var transactions = await _transactionRepository.GetFilteredAsync(
            userId, year, month, accountId
        );

        return transactions.Select(t => new TransactionResponseDto
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            AccountId = t.AccountId,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}
