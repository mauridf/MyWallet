using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Entities;

public class Transaction : Entity
{
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;

    protected Transaction() { }

    public Transaction(string description, decimal amount, TransactionType type, Guid accountId)
    {
        Description = description;
        Amount = amount;
        Type = type;
        AccountId = accountId;
    }
}
