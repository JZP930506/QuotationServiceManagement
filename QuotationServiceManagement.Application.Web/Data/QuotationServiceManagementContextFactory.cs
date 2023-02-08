using Microsoft.EntityFrameworkCore;
using QuotationServiceManagement.Infrastructure.Repositories;

namespace QuotationServiceManagement.Application.Web.Data;

public class QuotationServiceManagementContextFactory
{
    public QuotationServiceManagementContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuotationServiceManagementContext>();

        optionsBuilder.UseSqlite("Data Source=QuotationServiceManagement.db;Cache=Shared",
            o => o.MigrationsAssembly("QuotationServiceManagement.Application.WebApi"));
        return new QuotationServiceManagementContext(optionsBuilder.Options);
    }

}