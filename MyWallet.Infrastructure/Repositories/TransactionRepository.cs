using Microsoft.EntityFrameworkCore;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly MyWalletDbContext _context;

    public TransactionRepository(MyWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserAndPeriodAsync(
        Guid userId,
        DateTime start,
        DateTime end
    )
    {
        return await _context.Transactions
            .Include(t => t.Account)
            .Where(t =>
                t.Account.UserId == userId &&
                t.CreatedAt >= start &&
                t.CreatedAt <= end
            )
            .ToListAsync();
    }
}
