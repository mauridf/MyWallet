using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWallet.API.Swagger.Examples;
using MyWallet.Application.DTOs;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly MyWalletDbContext _dbContext;

    public UsersController(MyWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new user")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created", typeof(UserResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDto>> Create(CreateUserDto dto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User(dto.Name, dto.Email, passwordHash);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get user by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        return Ok(new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        });
    }
}
