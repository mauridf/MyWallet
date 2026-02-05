using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;

namespace MyWallet.Application.UseCases;

public class UserService : IUserService
{
    private readonly MyWalletDbContext _dbContext;

    public UserService(MyWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var emailExists = await _dbContext.Users
            .AnyAsync(u => u.Email == dto.Email);

        if (emailExists)
            throw new InvalidOperationException("Email already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User(dto.Name, dto.Email, passwordHash);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponseDto?> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        return user == null ? null : MapToResponse(user);
    }

    private static UserResponseDto MapToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}
