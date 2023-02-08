using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuotationServiceManagement.Application.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ContractController : CommonControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<ContractController> _logger;

        public ContractController(IMediator mediator, ILogger<ContractController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // [ProducesResponseType(typeof(IEnumerable<>), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // [HttpGet( "")]
        // public async Task<IActionResult> GetQuotationsAsync()
        // {
        //     var result= await _mediator.Send(new PickQuotationsCommand());
        //     
        //     return Succeed<IEnumerable<QuotationDto>>(result,StatusCodes.Status200OK);
        // }

    }
}