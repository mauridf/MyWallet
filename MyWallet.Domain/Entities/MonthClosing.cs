using MyWallet.Domain.Entities;

public class MonthClosing : Entity
{
    public Guid UserId { get; private set; }
    public string Month { get; private set; } = null!;
    public string Year { get; private set; } = null!;
    public decimal TotalIncome { get; private set; }
    public decimal TotalExpense { get; private set; }
    public decimal TotalInvestment { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime ClosedAt { get; protected set; }

    protected MonthClosing() { }

    public MonthClosing(Guid userId, string month, string year,
        decimal income, decimal expense, decimal investment, DateTime closedAt)
    {
        UserId = userId;
        Month = month;
        Year = year;
        TotalIncome = income;
        TotalExpense = expense;
        TotalInvestment = investment;
        Balance = income - expense - investment;
        ClosedAt = closedAt;
    }
}
