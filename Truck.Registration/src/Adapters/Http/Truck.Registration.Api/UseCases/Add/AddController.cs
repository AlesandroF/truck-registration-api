using MediatR;
using Microsoft.AspNetCore.Mvc;
using Truck.Registration.Application.UseCases.Add;

namespace Truck.Registration.Api.UseCases.Add
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}