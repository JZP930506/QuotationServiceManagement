using MediatR;
using QuotationServiceManagement.Domain.Share.Models;

namespace QuotationServiceManagement.Infrastructure.Repositories;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, QuotationServiceManagementContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}