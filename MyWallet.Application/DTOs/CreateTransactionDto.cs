using MyWallet.Domain.Enums;

public class CreateTransactionDto
{
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Guid AccountId { get; set; }
}
