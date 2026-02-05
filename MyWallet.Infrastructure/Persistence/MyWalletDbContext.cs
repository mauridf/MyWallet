using Microsoft.EntityFrameworkCore;

namespace MyWallet.Infrastructure.Persistence;

public class MyWalletDbContext : DbContext
{
    public MyWalletDbContext(DbContextOptions<MyWalletDbContext> options)
        : base(options)
    {
    }

    // DbSets virão depois
}
