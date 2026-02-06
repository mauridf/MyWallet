using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infrastructure.Persistence.Configurations;

public class MonthlySnapshotConfiguration : IEntityTypeConfiguration<MonthlySnapshot>
{
    public void Configure(EntityTypeBuilder<MonthlySnapshot> builder)
    {
        builder.ToTable("MonthlySnapshots");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Month)
            .IsRequired();

        builder.Property(x => x.TotalIncome)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalExpense)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Balance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.Year, x.Month })
            .IsUnique(); // snapshot único por mês
    }
}
