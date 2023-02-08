using FluentValidation;
using Microsoft.Extensions.Logging;
using QuotationServiceManagement.Application.Service.Quotation.Commands;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Validations
{
    public class CreateQuotationCommandValidator : AbstractValidator<CreateInquiryCommand>
    {
        public CreateQuotationCommandValidator(ILogger<CreateQuotationCommandValidator> logger)
        {
            RuleFor(command => command.BankAccount).NotEmpty();
            RuleFor(command => command.OpeningBank).NotEmpty();
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.Address).NotEmpty();
            RuleFor(command => command.Phone).NotEmpty().Length(11);
            RuleFor(command => command.Fax).NotEmpty();
            RuleFor(command => command.EndTime).NotEmpty();
            RuleFor(command => command.StartTime).NotEmpty();
            RuleFor(command => command.QuotationItems).Must(ContainQuotationItems).WithMessage("No quotation items found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool ContainQuotationItems(IEnumerable<QuotationItemsDto> quotationItemsDtos)
        {
            return quotationItemsDtos.Any();
        }
    }
}