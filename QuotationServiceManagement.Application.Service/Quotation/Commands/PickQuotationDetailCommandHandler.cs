using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

public class PickQuotationDetailCommandHandler : IRequestHandler<PickQuotationDetailCommand, QuotationDetailDto>
{
    private readonly IQuotationRepository _quotationRepository;

    private readonly IInquiryPartyRepository _inquiryPartyRepository;

    private readonly ILogger<PickQuotationCommandHandler> _logger;

    private readonly IMapper _mapper;

    public PickQuotationDetailCommandHandler(IQuotationRepository quotationRepository, ILogger<PickQuotationCommandHandler> logger, IMapper mapper,
        IInquiryPartyRepository inquiryPartyRepository)
    {
        _quotationRepository = quotationRepository;
        _logger = logger;
        _mapper = mapper;
        _inquiryPartyRepository = inquiryPartyRepository;
    }

    public async Task<QuotationDetailDto> Handle(PickQuotationDetailCommand request, CancellationToken cancellationToken)
    {
        var quotation = await _quotationRepository.GetAsync(request.QuotationId, cancellationToken);
        var inquiryParty = await _inquiryPartyRepository.GetByIdAsync(quotation.InquiryPartyId, cancellationToken);

        var quotationDetailDto = _mapper.Map<QuotationDetailDto>(quotation);
        quotationDetailDto.InquiryPartyDto = _mapper.Map<InquiryPartyDto>(inquiryParty);
        return quotationDetailDto;
    }
}