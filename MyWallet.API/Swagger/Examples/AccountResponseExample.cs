using MyWallet.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

public class AccountResponseExample : IExamplesProvider<AccountResponseDto>
{
    public AccountResponseDto GetExamples()
    {
        return new AccountResponseDto
        {
            Id = Guid.NewGuid(),
            Name = "Carteira Principal",
            Balance = 1500.75m
        };
    }
}
