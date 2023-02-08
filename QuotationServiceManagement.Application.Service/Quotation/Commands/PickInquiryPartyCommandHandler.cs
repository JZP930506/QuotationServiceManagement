using AutoMapper;
using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands
{
    public class PickInquiryPartyCommandHandler : IRequestHandler<PickInquiryPartyCommand, InquiryPartyDto>
    {
        private readonly IInquiryPartyRepository _inquiryPartyRepository;

        private readonly IMapper _mapper;

        public PickInquiryPartyCommandHandler(IInquiryPartyRepository inquiryPartyRepository, IMapper mapper)
        {
            _inquiryPartyRepository = inquiryPartyRepository;
            _mapper = mapper;
        }

        public async Task<InquiryPartyDto> Handle(PickInquiryPartyCommand request, CancellationToken cancellationToken)
        {
            var inquiryParty = await _inquiryPartyRepository.GetAsync(request.Title, cancellationToken);

            var result = _mapper.Map<InquiryPartyDto>(inquiryParty);
            return result;
        }
    }
}