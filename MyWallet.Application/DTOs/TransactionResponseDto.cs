using MyWallet.Domain.Enums;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}
