using MyWallet.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

public class AccountListExample : IExamplesProvider<List<AccountResponseDto>>
{
    public List<AccountResponseDto> GetExamples()
    {
        return new()
        {
            new() { Id = Guid.NewGuid(), Name = "Carteira Principal", Balance = 1500 },
            new() { Id = Guid.NewGuid(), Name = "Poupança", Balance = 3200 }
        };
    }
}
