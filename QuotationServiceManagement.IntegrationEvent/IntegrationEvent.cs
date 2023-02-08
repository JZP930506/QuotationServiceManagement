namespace QuotationServiceManagement.IntegrationEvent;

public abstract class IntegrationEvent
{
    public Guid MessageId { get; }

    public DateTime CreationTime { get; }

    protected IntegrationEvent()
    {
        MessageId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
}