using Swashbuckle.AspNetCore.Filters;

public class MonthlySnapshotExample : IExamplesProvider<MonthlySnapshotResponseDto>
{
    public MonthlySnapshotResponseDto GetExamples()
    {
        return new MonthlySnapshotResponseDto
        {
            Year = 2025,
            Month = 1,
            TotalIncome = 8000,
            TotalExpense = 4500,
            Balance = 3500
        };
    }
}
