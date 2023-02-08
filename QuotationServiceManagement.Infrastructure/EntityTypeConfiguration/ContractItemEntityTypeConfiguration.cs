using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;
using QuotationServiceManagement.Infrastructure.Repositories;

namespace QuotationServiceManagement.Infrastructure.EntityTypeConfiguration;

public class ContractItemEntityTypeConfiguration : IEntityTypeConfiguration<ContractItem>
{
    public void Configure(EntityTypeBuilder<ContractItem> builder)
    {
        builder.ToTable(nameof(ContractItem).ToLower(), QuotationServiceManagementContext.DEFAULT_SCHEMA);

        builder.HasKey(o => o.Id);

        builder.Ignore(b => b.DomainEvents);

        builder.Ignore(b => b.TotalPrice);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Quantity).IsRequired();

        builder.Property(x => x.Specification).IsRequired();

        builder.Property(x => x.Remark).IsRequired();

        builder.Property(x => x.TechnologicalStandard).IsRequired();

        builder.Property(x => x.UnitPrice).IsRequired();
    }
}
