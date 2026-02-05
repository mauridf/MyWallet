using MyWallet.Application.DTOs;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetMonthlySummary(Guid userId, int year, int month);
    Task<MonthlySnapshotResponseDto> CloseMonth(Guid userId, int year, int month);
    Task<IEnumerable<MonthlySnapshotResponseDto>> GetLastMonths(Guid userId, int months);
}
