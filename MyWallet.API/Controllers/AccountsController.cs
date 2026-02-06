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
public class AccountsController : BaseController
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
        var userId = GetUserId();
        var result = await _service.CreateAsync(dto, userId);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all accounts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<AccountResponseDto>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountListExample))]
    public async Task<ActionResult<List<AccountResponseDto>>> GetAll()
    {
        var userId = GetUserId();
        return Ok(await _service.GetAllAsync(userId));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get account by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(AccountResponseDto))]
    public async Task<ActionResult<AccountResponseDto>> GetById(Guid id)
    {
        var userId = GetUserId();
        return Ok(await _service.GetByIdAsync(id, userId));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update account")]
    [SwaggerResponse(StatusCodes.Status200OK, "Updated", typeof(AccountResponseDto))]
    public async Task<ActionResult<AccountResponseDto>> Update(Guid id, UpdateAccountDto dto)
    {
        var userId = GetUserId();
        return Ok(await _service.UpdateAsync(id, dto, userId));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deactivate account")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Deactivated")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        await _service.DeleteAsync(id, userId);
        return NoContent();
    }
}
