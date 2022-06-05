using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Truck.Registration.Api.UseCases.Put
{
    [Route("api/[controller]")]
    [ApiController]
    public class PutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync()
        {
            var response = await _mediator.Send(new GetCommand());

            return Ok(response);
        }
    }
}