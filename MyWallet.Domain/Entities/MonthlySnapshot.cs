namespace MyWallet.Domain.Entities;

public class MonthlySnapshot : Entity
{
    public Guid UserId { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }

    public decimal TotalIncome { get; private set; }
    public decimal TotalExpense { get; private set; }
    public decimal Balance { get; private set; }

    protected MonthlySnapshot() { }

    public MonthlySnapshot(Guid userId, int year, int month, decimal income, decimal expense)
    {
        UserId = userId;
        Year = year;
        Month = month;
        TotalIncome = income;
        TotalExpense = expense;
        Balance = income - expense;
    }
}
