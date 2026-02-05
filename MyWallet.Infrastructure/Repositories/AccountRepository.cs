using Microsoft.EntityFrameworkCore;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;

namespace MyWallet.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly MyWalletDbContext _context;

    public AccountRepository(MyWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Accounts
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid accountId, Guid userId)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId);
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}
