using DotNetCore.CAP;
using MediatR;
using QuotationServiceManagement.Application.Service.Contract.Commands;
using QuotationServiceManagement.Application.Service.IntegrationEvents;
using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.IntegrationEvent;

namespace QuotationServiceManagement.Application.Web.IntegrationEventHandler
{
    public class QuotationIntegrationEventSubscribe : IIntegrationEventSubscribe<QuotationFinishIntegrationEvent>, ICapSubscribe
    {
        private readonly IMediator _mediator;
        
        private ILogger<QuotationIntegrationEventSubscribe> _logger;

        public QuotationIntegrationEventSubscribe(IMediator mediator, ILogger<QuotationIntegrationEventSubscribe> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [CapSubscribe($"{nameof(QuotationFinishIntegrationEvent)}.Subscribe")]
        public async Task HandleMessage(QuotationFinishIntegrationEvent notification,
            CancellationToken cancellationToken)
        {
            // todo create the contract 
            var contractBuilder = new ContractBuilder();
            var command = await _mediator.Send(new CreateContractCommand(notification.QuotationId, notification.InquiryPartyId,
                    notification.SubmitTime), cancellationToken);
        }
    }
}