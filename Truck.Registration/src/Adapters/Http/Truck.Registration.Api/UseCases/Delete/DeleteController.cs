using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Truck.Registration.Api.UseCases.Delete
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var response = await _mediator.Send(new GetCommand());

            return Ok(response);
        }
    }
}