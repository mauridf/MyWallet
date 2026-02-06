using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;

public class MonthlySnapshotRepository : IMonthlySnapshotRepository
{
    private readonly MyWalletDbContext _context;

    public MonthlySnapshotRepository(MyWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MonthlySnapshot snapshot)
    {
        _context.MonthlySnapshots.Add(snapshot);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MonthlySnapshot>> GetLastMonthsAsync(Guid userId, int months)
    {
        return await _context.MonthlySnapshots
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .Take(months)
            .ToListAsync();
    }
}
