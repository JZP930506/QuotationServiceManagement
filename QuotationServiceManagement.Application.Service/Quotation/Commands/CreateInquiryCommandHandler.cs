using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;
using QuotationServiceManagement.Domain.Share.Models;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands
{
    public class CreateInquiryCommandHandler : IRequestHandler<CreateInquiryCommand, int>
    {
        private readonly IInquiryPartyRepository _inquiryPartyRepository;

        private readonly IQuotationRepository _quotationRepository;

        private readonly ILogger<CreateInquiryCommandHandler> _logger;

        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public CreateInquiryCommandHandler(
            IQuotationRepository quotationRepository,
            IInquiryPartyRepository inquiryPartyRepository,
            ILogger<CreateInquiryCommandHandler> logger,
            IMediator mediator, IMapper mapper)
        {
            _quotationRepository = quotationRepository;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _inquiryPartyRepository = inquiryPartyRepository;
        }

        public async Task<int> Handle(CreateInquiryCommand request, CancellationToken cancellationToken)
        {
            var existInquiryParty = await _mediator.Send(new PickInquiryPartyCommand(request.Title), cancellationToken);
            if (existInquiryParty is null)
            {
                var inquiryParty = new InquiryParty(request.Title, request.Address,
                    new LinkInfo(request.LinkMan, request.Email, request.Phone, request.Fax)
                    , new BankInfo(request.BankAccount, request.OpeningBank));
                _inquiryPartyRepository.Add(inquiryParty);
                await _inquiryPartyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                existInquiryParty = _mapper.Map<InquiryPartyDto>(inquiryParty);
            }
            var quotation = new Domain.AggregatesModel.InquiryAggregate.QuotationAggregate.Quotation();
            quotation.SetInquiryParty(existInquiryParty.Id);
            quotation.InitQuotation(request.StartTime, request.Description, request.TotalData);
            foreach (var item in request.QuotationItems)
            {
                quotation.AddQuotationItem(item.Name, item.Specification, item.TechnologicalStandard, item.UnitPrice, item.Quatity, item.Remark);
            }
            _quotationRepository.Add(quotation);
            await _quotationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("----- Creating Quotation - Quotation: {@Quotation}", quotation);
            return await Task.FromResult(quotation.Id);
        }
    }
}