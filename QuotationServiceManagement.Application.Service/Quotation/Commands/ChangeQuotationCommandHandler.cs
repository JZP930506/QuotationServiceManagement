using MediatR;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands
{
    public class ChangeQuotationCommandHandler : IRequestHandler<ChangeQuotationCommand, int>
    {
        private readonly IInquiryPartyRepository _inquiryPartyRepository;

        private readonly IQuotationRepository _quotationRepository;

        private readonly ILogger<ChangeQuotationCommandHandler> _logger;

        public ChangeQuotationCommandHandler(
            IInquiryPartyRepository inquiryPartyRepository,
            IQuotationRepository quotationRepository,
            ILogger<ChangeQuotationCommandHandler> logger)
        {
            _inquiryPartyRepository = inquiryPartyRepository;
            _quotationRepository = quotationRepository;
            _logger = logger;
        }

        public async Task<int> Handle(ChangeQuotationCommand request, CancellationToken cancellationToken)
        {
            var quotation = await _quotationRepository.GetAsync(request.Id, cancellationToken);
            if (quotation is null)
                throw new ArgumentNullException($"{quotation} not exist by {request.Id}");
            foreach (var item in request.QuotationItems)
            {
                quotation.ChangeQuotationItem(item.Name, item.Specification, item.TechnologicalStandard, item.UnitPrice, item.Quatity, item.Remark);
            }
            quotation.ChangeQuotation(request.StartTime, request.EndTime);

            _quotationRepository.Update(quotation);
            await _quotationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("----- Update Quotation - Quotation: {@Quotation}", quotation);
            return await Task.FromResult(quotation.Id);
        }
    }
}