using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.Domain.Share.Enums;
using QuotationServiceManagement.Infrastructure.Repositories;

namespace QuotationServiceManagement.Infrastructure.EntityTypeConfiguration;

public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable(
            nameof(Contract).ToLower(),
            QuotationServiceManagementContext.DEFAULT_SCHEMA
        );

        builder.HasKey(o => o.Id);

        builder.Ignore(b => b.DomainEvents);

        builder.Property(x => x.Gcid)
            .HasMaxLength(40).IsRequired();

        builder.Property(x => x.QuotationId)
            .IsRequired();

        builder.Property(x => x.ContractNo)
            .IsRequired();

        builder.Property(x => x.CreateTime)
            .IsRequired();

        builder.Property(x => x.DeliveryTime)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasDefaultValue(string.Empty)
            .IsRequired();

        builder.Property(x => x.TotalData)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasDefaultValue(0)
            .IsRequired();

        builder.OwnsOne(x => x.FirstParty,
            a =>
            { 
                a.WithOwner();
            });

        builder.HasMany(b => b.ContractItems)
            .WithOne()
            .HasForeignKey("ContractId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}