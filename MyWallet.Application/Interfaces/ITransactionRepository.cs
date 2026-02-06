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
    Task<IEnumerable<Transaction>> GetFilteredAsync(
        Guid userId,
        int? year,
        int? month,
        Guid? accountId
    );
}
