using DotNetCore.CAP;
using MediatR;
using QuotationServiceManagement.Application.Service.IntegrationEvents;
using QuotationServiceManagement.Domain.Event;

namespace QuotationServiceManagement.Application.Service.Quotation.DomainEventHandler
{
    public class QuotationFinishDomainEventHandler : INotificationHandler<QuotationFinishDomainEvent>
    {
        private readonly ICapPublisher _capPublisher;

        public QuotationFinishDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(QuotationFinishDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capPublisher.PublishAsync(
                "QuotationFinishIntegrationEvent.Subscribe",
                new QuotationFinishIntegrationEvent(notification.QuotationId, notification.InquiryPartyId, DateTime.Now),
                cancellationToken: cancellationToken);
        }
    }
}