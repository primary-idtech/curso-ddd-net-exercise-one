using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Operation;

namespace ROFE.Infrastructure.ORM.Config;

public class StockOperationConfiguration : IEntityTypeConfiguration<StockOperation>
{
    public void Configure(EntityTypeBuilder<StockOperation> builder)
    {
        builder.ToTable("StockOperations");
        builder.HasBaseType<Operation>();

        builder.Property(e => e.Quantity).IsRequired().HasColumnOrder(1);
        builder.Property(e => e.InstrumentId).IsRequired().HasColumnOrder(2);
        builder.Property(e => e.TradeAgentId).IsRequired().HasColumnOrder(3);
        builder.Property(e => e.TradeDate).IsRequired().HasColumnOrder(4);
        builder.Property(e => e.SettlementDate).IsRequired().HasColumnOrder(5);
    }
}
