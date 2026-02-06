using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces;
using MyWallet.Infrastructure.Persistence;

public class MonthClosingRepository : IMonthClosingRepository
{
    private readonly MyWalletDbContext _context;

    public MonthClosingRepository(MyWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MonthClosing closing)
    {
        _context.MonthClosings.Add(closing);
        await _context.SaveChangesAsync();
    }

    public async Task<MonthClosing?> GetByMonthAsync(Guid userId, string year, string month)
    {
        return await _context.MonthClosings
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.Year == year &&
                x.Month == month
            );
    }
}
