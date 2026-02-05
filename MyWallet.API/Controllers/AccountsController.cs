using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Controllers;

[Authorize]
[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new account")]
    [SwaggerResponse(StatusCodes.Status201Created, "Account created", typeof(AccountResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(AccountResponseExample))]
    public async Task<ActionResult<AccountResponseDto>> Create(CreateAccountDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CreateAsync(dto, userId);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all accounts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<AccountResponseDto>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountListExample))]
    public async Task<ActionResult<List<AccountResponseDto>>> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _service.GetAllAsync(userId));
    }
}
