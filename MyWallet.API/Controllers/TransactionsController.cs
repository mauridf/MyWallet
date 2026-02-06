using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.API.Swagger.Examples;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Controllers;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a transaction")]
    [SwaggerResponse(StatusCodes.Status201Created, "Transaction created", typeof(TransactionResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(TransactionResponseExample))]
    public async Task<ActionResult<TransactionResponseDto>> Create(CreateTransactionDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CreateAsync(dto, userId);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get transactions")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<TransactionResponseDto>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TransactionListExample))]
    public async Task<ActionResult<List<TransactionResponseDto>>> Get(
        [FromQuery] int? year,
        [FromQuery] int? month,
        [FromQuery] Guid? accountId
    )
    {
        var userId = GetUserId();

        if (!year.HasValue && !month.HasValue && !accountId.HasValue)
            return Ok(await _service.GetAllAsync(userId));

        return Ok(await _service.GetFilteredAsync(userId, year, month, accountId));
    }
}
