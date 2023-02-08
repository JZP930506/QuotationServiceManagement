using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;
using QuotationServiceManagement.Domain.Interface;
using QuotationServiceManagement.Infrastructure.Repositories;

namespace QuotationServiceManagement.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, QuotationServiceManagementContext>(provider =>
            provider.GetService<QuotationServiceManagementContext>()
            );

        services.AddScoped<IQuotationRepository, QuotationRepository>();

        services.AddScoped<IInquiryPartyRepository, InquiryPartyRepository>();

        services.AddScoped<IContractRepository, ContractRepository>();

        return services;
    }

}