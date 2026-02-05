namespace MyWallet.Domain.Entities;

public class Account : Entity
{
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public Guid UserId { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    protected Account() { }

    public Account(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
        Balance = 0;
    }

    public void Credit(decimal amount)
    {
        Balance += amount;
    }

    public void Debit(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance");

        Balance -= amount;
    }

    public void AddBalance(decimal amount)
    {
        Balance += amount;
    }

    public void SubtractBalance(decimal amount)
    {
        Balance -= amount;
    }
}
