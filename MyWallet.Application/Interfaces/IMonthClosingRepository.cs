using MyWallet.Domain.Entities;

public interface IMonthClosingRepository
{
    Task AddAsync(MonthClosing closing);
    Task<MonthClosing?> GetByMonthAsync(Guid userId, string year, string month);
}
