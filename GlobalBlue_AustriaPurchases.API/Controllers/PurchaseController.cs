using Microsoft.AspNetCore.Mvc;
using MediatR;
using GlobalBlue_AustriaPurchases.API.Presentation;
using GlobalBlue_AustriaPurchases.API.Queries.CalculatePurchase;

namespace GlobalBlue_AustriaPurchases.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator) { 
            _mediator = mediator;
        }

        [HttpGet("calculatePurchased")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CalculatePurchasePresentation>> Get([FromQuery] CalculatePurchaseQuery data)
        {
            var result = await _mediator.Send(data);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}