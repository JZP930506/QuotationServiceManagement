using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuotationServiceManagement.Application.Service.Quotation.Commands;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class InquiryController : CommonControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<InquiryController> _logger;

        public InquiryController(IMediator mediator, ILogger<InquiryController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [ProducesResponseType(typeof(QuotationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateInquiryCommand createInquiryCommand)
        {
            var quotationId = await _mediator.Send(createInquiryCommand);

            var result = await _mediator.Send(new PickQuotationCommand(quotationId));

            return Succeed<QuotationDto>(result, StatusCodes.Status201Created);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Change")]
        public async Task<IActionResult> Change([FromBody] ChangeQuotationCommand createInquiryCommand)
        {
            var quotationId = await _mediator.Send(createInquiryCommand);
            var result = await _mediator.Send(new PickQuotationCommand(quotationId));
            return Succeed<QuotationDto>(result, StatusCodes.Status200OK);
        }

        [ProducesResponseType(typeof(IEnumerable<QuotationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]
        public async Task<IActionResult> GetQuotationsAsync()
        {
            var result = await _mediator.Send(new PickQuotationsCommand());

            return Succeed<IEnumerable<QuotationDto>>(result, StatusCodes.Status200OK);
        }

        [ProducesResponseType(typeof(QuotationDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotationDetailAsync([FromRoute] int id)
        {
            var result = await _mediator.Send(new PickQuotationDetailCommand(id));

            return Succeed<QuotationDetailDto>(result, StatusCodes.Status200OK);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("Finish")]
        public async Task<IActionResult> Finish([FromQuery] int id)
        {
            var result = await _mediator.Send(new FinishQuotationCommand(id));

            return Succeed("Succeed");
        }
    }
}