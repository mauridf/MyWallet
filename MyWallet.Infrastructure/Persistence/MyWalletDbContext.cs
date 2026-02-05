using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;

namespace MyWallet.Infrastructure.Persistence;

public class MyWalletDbContext : DbContext
{
    public MyWalletDbContext(DbContextOptions<MyWalletDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyWalletDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
