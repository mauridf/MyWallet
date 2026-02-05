using MyWallet.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Swagger.Examples;

public class UserResponseExample : IExamplesProvider<UserResponseDto>
{
    public UserResponseDto GetExamples()
    {
        return new UserResponseDto
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Name = "João da Silva",
            Email = "joao@email.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}
