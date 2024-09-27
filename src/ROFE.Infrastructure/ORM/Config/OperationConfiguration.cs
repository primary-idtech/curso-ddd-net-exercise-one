using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ROFE.Domain.Models.Operation;

namespace ROFE.Infrastructure.ORM.Config;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.Property(e => e.Id)
            .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd()
            .HasColumnOrder(1);

        builder.OwnsOne(e => e.Type, nameBuilder =>
        {
            nameBuilder.Property(n => n.Id)
                .IsRequired()
                .HasColumnName("TypeId")
                .HasColumnOrder(2);
        });

        builder.Property(e => e.UserId).IsRequired().HasColumnOrder(3);
        builder.Property(e => e.PortfolioId).IsRequired().HasColumnOrder(4);

        builder.OwnsOne(e => e.Price, priceBuilder =>
        {
            priceBuilder.Property(n => n.Amount)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnOrder(5);
            
            priceBuilder.Property(n => n.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasColumnOrder(6);
        });


        builder.HasKey(e => e.Id).HasName("PK_Operation_Id");
    }
}
