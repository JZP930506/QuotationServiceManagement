using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

public class PickQuotationsCommandHandler : IRequestHandler<PickQuotationsCommand, IEnumerable<QuotationDto>>
{
    private readonly IQuotationRepository _quotationRepository;

    private readonly ILogger<PickQuotationCommandHandler> _logger;

    private readonly IMapper _mapper;

    public PickQuotationsCommandHandler(IQuotationRepository quotationRepository, ILogger<PickQuotationCommandHandler> logger, IMapper mapper)
    {
        _quotationRepository = quotationRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuotationDto>> Handle(PickQuotationsCommand request, CancellationToken cancellationToken)
    {
        var quotations = await _quotationRepository.GetListAsync(cancellationToken);

        return quotations.Select(t => _mapper.Map<QuotationDto>(t));
    }
}