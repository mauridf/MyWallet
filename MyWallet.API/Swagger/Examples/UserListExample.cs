using MyWallet.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

public class UserListExample : IExamplesProvider<List<UserResponseDto>>
{
    public List<UserResponseDto> GetExamples()
    {
        return new List<UserResponseDto>
        {
            new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Name = "Mauricio",
                Email = "mauricio@email.com",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
