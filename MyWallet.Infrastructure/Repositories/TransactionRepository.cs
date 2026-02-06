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

    public async Task<IEnumerable<Transaction>> GetFilteredAsync(
    Guid userId,
    int? year,
    int? month,
    Guid? accountId
)
    {
        var query = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.UserId == userId)
            .AsQueryable();

        if (year.HasValue && month.HasValue)
        {
            var startDate = new DateTime(year.Value, month.Value, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            query = query.Where(t =>
                t.CreatedAt >= startDate &&
                t.CreatedAt <= endDate
            );
        }

        if (accountId.HasValue)
            query = query.Where(t => t.AccountId == accountId);

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
