using MyWallet.Domain.Entities;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAllByUserAsync(Guid userId);
    Task<IEnumerable<Transaction>> GetByUserAndPeriodAsync(
        Guid userId,
        DateTime start,
        DateTime end
    );
    Task<IEnumerable<Transaction>> GetByFilteredAsync(
        Guid userId,
        DateTime? start,
        DateTime? end,
        Guid? accountId
    );
}
