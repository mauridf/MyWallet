using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MyWallet.API.Swagger.Examples;

namespace MyWallet.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new user")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created", typeof(UserResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDto>> Create(CreateUserDto dto)
    {
        try
        {
            var result = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get user by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(UserResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
