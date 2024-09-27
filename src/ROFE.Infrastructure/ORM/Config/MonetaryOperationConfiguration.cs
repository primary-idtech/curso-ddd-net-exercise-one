using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Operation;

namespace ROFE.Infrastructure.ORM.Config;

public class MonetaryOperationConfiguration : IEntityTypeConfiguration<MonetaryOperation>
{
    public void Configure(EntityTypeBuilder<MonetaryOperation> builder)
    {
        builder.ToTable("MonetaryOperations");
        builder.HasBaseType<Operation>();

        builder.Property(e => e.Comment).HasColumnOrder(1);
    }
}
