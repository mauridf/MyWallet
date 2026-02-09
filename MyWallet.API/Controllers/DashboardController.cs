using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MyWallet.API.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public class DashboardController : BaseController
{
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service)
    {
        _service = service;
    }

    [HttpGet("monthly")]
    [SwaggerOperation(Summary = "Get monthly financial summary")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(MonthlySnapshotResponseDto))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MonthlySnapshotExample))]
    public async Task<ActionResult<MonthlySnapshotResponseDto>> GetMonthly(
        [FromQuery] int year,
        [FromQuery] int month)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _service.GetMonthlySummary(GetUserId(), year, month));
    }

    [HttpPost("close-month")]
    [SwaggerOperation(Summary = "Close month and persist snapshot")]
    public async Task<ActionResult<MonthlySnapshotResponseDto>> CloseMonth(
        [FromQuery] int year,
        [FromQuery] int month)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CloseMonth(userId, year, month);
        return Ok(result);
    }

    [HttpGet("export/csv")]
    [SwaggerOperation(Summary = "Export dashboard data as CSV")]
    public async Task<IActionResult> ExportCsv([FromQuery] int months = 6)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var snapshots = await _service.GetLastMonths(userId, months);

        var csv = new StringBuilder();
        csv.AppendLine("Year,Month,Income,Expense,Balance");

        foreach (var s in snapshots)
            csv.AppendLine($"{s.Year},{s.Month},{s.TotalIncome},{s.TotalExpense},{s.Balance}");

        return File(
            Encoding.UTF8.GetBytes(csv.ToString()),
            "text/csv",
            "dashboard.csv"
        );
    }

    [HttpGet("history")]
    [SwaggerOperation(Summary = "Get dashboard history")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<MonthlySnapshotResponseDto>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MonthlySnapshotListExample))]
    public async Task<IActionResult> GetHistory([FromQuery] int months = 6)
    {
        var userId = GetUserId();
        var result = await _service.GetHistory(userId, months);
        return Ok(result);
    }
}
