using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Portfolio;

namespace ROFE.Infrastructure.ORM.Config;

public class PortfolioInstrumentConfiguration : IEntityTypeConfiguration<PortfolioInstrument>
{
    public void Configure(EntityTypeBuilder<PortfolioInstrument> builder)
    {
        builder.ToTable("PortfolioInstruments");

        builder.Property(e => e.Id)
            .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd()
            .HasColumnOrder(1);

        builder.Property(e => e.InstrumentId).IsRequired().HasColumnOrder(2);

        builder.OwnsOne(e => e.AveragePurchasePrice, averagePurchasePriceBuilder =>
        {
            averagePurchasePriceBuilder.Property(n => n.Quantity)
                .HasColumnName("Quantity")
                .IsRequired()
                .HasColumnOrder(3);

            averagePurchasePriceBuilder.Property(n => n.Amount)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnOrder(4);

            averagePurchasePriceBuilder.Property(n => n.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasColumnOrder(5);
        });

        builder.HasKey(e => e.Id).HasName("PK_PortfolioInstrument_Id");
    }
}