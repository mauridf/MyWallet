using MyWallet.Application.DTOs;
using MyWallet.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Swagger.Examples;

public class TransactionResponseExample : IExamplesProvider<TransactionResponseDto>
{
    public TransactionResponseDto GetExamples()
    {
        return new TransactionResponseDto
        {
            Id = Guid.NewGuid(),
            Description = "Salary",
            Amount = 5000,
            Type = TransactionType.Income,
            CreatedAt = DateTime.UtcNow
        };
    }
}
