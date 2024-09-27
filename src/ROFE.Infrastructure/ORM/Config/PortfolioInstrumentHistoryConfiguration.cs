using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Portfolio;

namespace ROFE.Infrastructure.ORM.Config;

public class PortfolioInstrumentHistoryConfiguration : IEntityTypeConfiguration<PortfolioInstrumentHistory>
{
    public void Configure(EntityTypeBuilder<PortfolioInstrumentHistory> builder)
    {
        builder.Property(e => e.Id)
            .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd()
            .HasColumnOrder(1);

        builder.Property(e => e.InstrumentId).IsRequired().HasColumnOrder(2);
        builder.Property(e => e.Quantity).IsRequired().HasColumnOrder(3);
        builder.Property(e => e.Amount).IsRequired().HasColumnOrder(4);
        builder.Property(e => e.Currency).IsRequired().HasColumnOrder(5);
        builder.Property(e => e.CreatedAt).IsRequired().HasColumnOrder(6);

        builder.HasKey(e => e.Id).HasName("PK_PortfolioInstrumentHistory_Id");
    }
}