using MediatR;
using Microsoft.AspNetCore.Mvc;
using Truck.Registration.Application.UseCases.Put;

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
        public async Task<IActionResult> PutAsync([FromBody] PutCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}