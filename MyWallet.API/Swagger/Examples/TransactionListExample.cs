using MyWallet.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

public class TransactionListExample : IExamplesProvider<List<TransactionResponseDto>>
{
    public List<TransactionResponseDto> GetExamples()
    {
        return new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Salário",
                Amount = 5000,
                Type = TransactionType.Income,
                AccountId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Aluguel",
                Amount = 1500,
                Type = TransactionType.Expense,
                AccountId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
