using Microsoft.EntityFrameworkCore;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;

namespace MyWallet.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyWalletDbContext _dbContext;

    public UserRepository(MyWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .Where(u => u.IsActive)
            .ToListAsync();
    }
}
