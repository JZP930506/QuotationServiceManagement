using System.Reflection;
using DotNetCore.CAP;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuotationServiceManagement.Application.Service.Quotation.Behaviors;
using QuotationServiceManagement.Application.Service.Quotation.Commands;
using QuotationServiceManagement.Application.Web.IntegrationEventHandler;
using QuotationServiceManagement.Infrastructure.Repositories;
using Savorboard.CAP.InMemoryMessageQueue;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiDependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateInquiryCommand).Assembly);
            services.AddAutoMapper(typeof(CreateInquiryCommand).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddScoped<ICapSubscribe, QuotationIntegrationEventSubscribe>();

            services.AddDbContext<QuotationServiceManagementContext>(options =>
                {
                    options.UseSqlite("Data Source=QuotationServiceManagement.db;Cache=Shared",
                        sqliteOptionsAction =>
                        {
                            sqliteOptionsAction.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                        });
                }
            );

            services.AddCap(x =>
            {
                x.UseInMemoryStorage();
                x.UseRabbitMQ(rb =>
                {
                    rb.HostName = "localhost";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                    rb.Port = 5672;
                    rb.VirtualHost = "/";
                });
                //启用仪表盘
                x.UseDashboard();
                x.FailedRetryCount = 5; //
                x.FailedRetryInterval = 60 * 2;
            });
            return services;
        }
    }
}