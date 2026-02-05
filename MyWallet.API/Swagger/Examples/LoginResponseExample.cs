using MyWallet.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Swagger.Examples;

public class LoginResponseExample : IExamplesProvider<LoginResponseDto>
{
    public LoginResponseDto GetExamples()
    {
        return new LoginResponseDto
        {
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }
}
