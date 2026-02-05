using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;

namespace MyWallet.Application.UseCases;

public class DashboardService : IDashboardService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMonthlySnapshotRepository _snapshotRepository;

    public DashboardService(
        ITransactionRepository transactionRepository,
        IMonthlySnapshotRepository snapshotRepository)
    {
        _transactionRepository = transactionRepository;
        _snapshotRepository = snapshotRepository;
    }

    public async Task<DashboardSummaryDto> GetMonthlySummary(Guid userId, int year, int month)
    {
        var start = new DateTime(year, month, 1);
        var end = start.AddMonths(1).AddTicks(-1);

        var transactions = await _transactionRepository
            .GetByUserAndPeriodAsync(userId, start, end);

        var income = transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var expense = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        return new DashboardSummaryDto
        {
            TotalIncome = income,
            TotalExpense = expense,
            Balance = income - expense
        };
    }

    public async Task<MonthlySnapshotResponseDto> CloseMonth(Guid userId, int year, int month)
    {
        var summary = await GetMonthlySummary(userId, year, month);

        var snapshot = new MonthlySnapshot(
            userId,
            year,
            month,
            summary.TotalIncome,
            summary.TotalExpense
        );

        await _snapshotRepository.AddAsync(snapshot);

        return new MonthlySnapshotResponseDto
        {
            Year = snapshot.Year,
            Month = snapshot.Month,
            TotalIncome = snapshot.TotalIncome,
            TotalExpense = snapshot.TotalExpense,
            Balance = snapshot.Balance
        };
    }

    public async Task<IEnumerable<MonthlySnapshotResponseDto>> GetLastMonths(Guid userId, int months)
    {
        var snapshots = await _snapshotRepository.GetLastMonthsAsync(userId, months);

        return snapshots.Select(s => new MonthlySnapshotResponseDto
        {
            Year = s.Year,
            Month = s.Month,
            TotalIncome = s.TotalIncome,
            TotalExpense = s.TotalExpense,
            Balance = s.Balance
        });
    }
}
