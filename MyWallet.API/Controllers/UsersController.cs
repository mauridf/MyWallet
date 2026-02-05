using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWallet.Application.DTOs;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User(dto.Name, dto.Email, passwordHash);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            new { user.Id, user.Name, user.Email });
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get user by id")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.IsActive,
            user.CreatedAt
        });
    }
}
