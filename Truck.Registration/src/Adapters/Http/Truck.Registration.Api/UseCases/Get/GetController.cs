using MediatR;
using Microsoft.AspNetCore.Mvc;
using Truck.Registration.Application.UseCases.Get;

namespace Truck.Registration.Api.UseCases.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetCommand());

            return Ok(response);
        }
    }
}