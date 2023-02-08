using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Infrastructure.Repositories;

namespace QuotationServiceManagement.Infrastructure.EntityTypeConfiguration
{
    class InquiryPartyEntityTypeConfiguration : IEntityTypeConfiguration<InquiryParty>
    {
        public void Configure(EntityTypeBuilder<InquiryParty> builder)
        {
            builder.ToTable(nameof(InquiryParty).ToLower(), QuotationServiceManagementContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(x => x.Gcid)
                .HasMaxLength(40).IsRequired();

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Address).IsRequired();

            builder.OwnsOne(x => x.LinkInfo,
                a =>
                {
                    a.WithOwner();
                });

            builder.OwnsOne(x => x.BankInfo,
                a =>
                {
                    a.WithOwner();
                });
        }
    }
}