using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
