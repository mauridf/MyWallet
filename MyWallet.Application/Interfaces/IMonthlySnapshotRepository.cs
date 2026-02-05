using MyWallet.Domain.Entities;

public interface IMonthlySnapshotRepository
{
    Task AddAsync(MonthlySnapshot snapshot);
    Task<IEnumerable<MonthlySnapshot>> GetLastMonthsAsync(Guid userId, int months);
}
