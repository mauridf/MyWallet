using System.Security.Principal;
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

    public async Task<AccountResponseDto> GetByIdAsync(Guid id, Guid userId)
    {
        var account = await _repository.GetByIdAsync(id, userId);
        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance
        };
    }

    public async Task<AccountResponseDto> UpdateAsync(Guid id, UpdateAccountDto dto, Guid userId)
    {
        var account = await _repository.GetByIdAsync(id, userId);
        account.Update(dto.Name);
        await _repository.UpdateAsync(account);
        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance
        };
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var account = await _repository.GetByIdAsync(id, userId);
        account.Deactivate();
        await _repository.UpdateAsync(account);
    }

}
