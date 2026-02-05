using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.UseCases;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<AccountResponseDto> CreateAsync(CreateAccountDto dto, Guid userId)
    {
        var account = new Account(dto.Name, userId);
        await _repository.AddAsync(account);

        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance
        };
    }

    public async Task<List<AccountResponseDto>> GetAllAsync(Guid userId)
    {
        var accounts = await _repository.GetByUserIdAsync(userId);

        return accounts.Select(a => new AccountResponseDto
        {
            Id = a.Id,
            Name = a.Name,
            Balance = a.Balance
        }).ToList();
    }
}
