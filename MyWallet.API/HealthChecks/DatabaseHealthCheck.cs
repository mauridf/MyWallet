using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyWallet.Infrastructure.Persistence;

namespace MyWallet.API.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly MyWalletDbContext _dbContext;

    public DatabaseHealthCheck(MyWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? HealthCheckResult.Healthy("Database connection is OK")
            : HealthCheckResult.Unhealthy("Database connection failed");
    }
}
