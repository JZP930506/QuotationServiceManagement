using DotNetCore.CAP;
using MediatR;
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
        public Task HandleMessage(QuotationFinishIntegrationEvent notification, CancellationToken cancellationToken)
        {
            // todo create the contract 
            var contract = new ContractBuilder();
            contract.InitContractTime(notification.SubmitTime, notification.QuotationId, "", notification.TotalData);

            Console.WriteLine($"Quotation Id :{notification.QuotationId}");
            return Task.CompletedTask;
        }
    }
}