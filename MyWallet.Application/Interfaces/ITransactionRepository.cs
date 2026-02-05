using MyWallet.Domain.Entities;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetByUserAndPeriodAsync(
        Guid userId,
        DateTime start,
        DateTime end
    );
}
