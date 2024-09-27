using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Portfolio;

namespace ROFE.Infrastructure.ORM.Config;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnOrder(1);

        builder.OwnsOne(e => e.Balance, balanceBuilder =>
        {
            balanceBuilder.Property(n => n.Amount)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnOrder(2);

            balanceBuilder.Property(n => n.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasColumnOrder(3);
        });

        builder.HasKey(e => e.Id).HasName("PK_Portfolio_Id");
    }
}
