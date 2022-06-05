using MediatR;
using Microsoft.AspNetCore.Mvc;
using Truck.Registration.Application.UseCases.Delete;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(
                new DeleteCommand
                {
                    Id = id,
                });

            return Ok(response);
        }
    }
}