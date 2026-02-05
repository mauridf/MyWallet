using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<List<Account>> GetByUserIdAsync(Guid userId);
}
