using MediatR;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Application.Service.Quotation.Commands;

namespace QuotationServiceManagement.Application.Service.Contract.Commands
{
    public class CreateContractCommandHandler: IRequestHandler<CreateContractCommand, int>
    {
        private readonly IMediator _mediator;

        private readonly ILogger<CreateContractCommandHandler> _logger;

        public CreateContractCommandHandler(IMediator mediator, ILogger<CreateContractCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<int> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var quotationDetail = await _mediator.Send(new PickQuotationDetailCommand(request.QuotationId),cancellationToken);
            // var result= _mediator.Publish()
            throw new NotImplementedException();
        }
    }
}