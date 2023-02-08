namespace QuotationServiceManagement.IntegrationEvent;

public interface IIntegrationEventSubscribe<in T> where T : IntegrationEvent
{
    Task HandleMessage(T notification, CancellationToken cancellationToken);
}