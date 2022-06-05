using MediatR;
using Microsoft.AspNetCore.Mvc;
using Truck.Registration.Application.UseCases.GetById;

namespace Truck.Registration.Api.UseCases.GetById
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetByIdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetByIdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _mediator.Send(new GetByIdCommand { Id = id });

            return Ok(response);
        }
    }
}