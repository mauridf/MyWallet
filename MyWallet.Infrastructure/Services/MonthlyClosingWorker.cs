using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWallet.Application.Interfaces;

public class MonthlyClosingWorker : BackgroundService
{
    private readonly IServiceProvider _provider;

    public MonthlyClosingWorker(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;

            // roda só no dia 1 às 02:00
            if (now.Day == 1 && now.Hour == 2)
            {
                using var scope = _provider.CreateScope();
                var dashboardService = scope.ServiceProvider.GetRequiredService<IDashboardService>();
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var users = await userRepo.GetAllAsync();

                var lastMonth = now.AddMonths(-1);

                foreach (var user in users)
                {
                    await dashboardService.CloseMonth(
                        user.Id,
                        lastMonth.Year,
                        lastMonth.Month
                    );
                }

                // evita rodar múltiplas vezes no mesmo dia
                await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}
