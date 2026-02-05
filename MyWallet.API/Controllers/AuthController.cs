using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MyWallet.API.Swagger.Examples;

namespace MyWallet.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Authenticate user and generate JWT")]
    [SwaggerResponse(StatusCodes.Status200OK, "Login successful", typeof(LoginResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
