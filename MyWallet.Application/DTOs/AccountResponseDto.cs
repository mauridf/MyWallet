namespace MyWallet.Application.DTOs;

public class AccountResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
}
