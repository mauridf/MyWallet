using Swashbuckle.AspNetCore.Filters;

public class MonthlySnapshotListExample : IExamplesProvider<List<MonthlySnapshotResponseDto>>
{
    public List<MonthlySnapshotResponseDto> GetExamples()
    {
        return new()
        {
            new MonthlySnapshotResponseDto
            {
                Year = 2026,
                Month = 1,
                TotalIncome = 8000,
                TotalExpense = 4500,
                Balance = 3500
            },
            new MonthlySnapshotResponseDto
            {
                Year = 2026,
                Month = 2,
                TotalIncome = 8200,
                TotalExpense = 4700,
                Balance = 3500
            }
        };
    }
}
