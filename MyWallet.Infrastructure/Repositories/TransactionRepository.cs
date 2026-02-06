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

    public async Task<IEnumerable<Transaction>> GetAllByUserAsync(Guid userId)
    {
        return await _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByFilteredAsync(Guid userId, DateTime? start, DateTime? end, Guid? accountId)
    {
        var query = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.UserId == userId)
            .AsQueryable();

        if (year.HasValue && month.HasValue)
        {
            var start = new DateTime(year.Value, month.Value, 1);
            var end = start.AddMonths(1).AddTicks(-1);
            query = query.Where(t => t.CreatedAt >= start && t.CreatedAt <= end);
        }

        if (accountId.HasValue)
            query = query.Where(t => t.AccountId == accountId);

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
