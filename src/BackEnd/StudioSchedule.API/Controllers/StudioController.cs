using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Studio;
using StudioSchedule.Application.Queries.Studio;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public StudioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateStudio request, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(request, cancellationToken);
            return Ok(command);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStudios query ,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new GetStudioById(id));
            return Ok(command);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateStudioRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateStudioCommand(id, request.Name, request.Address, request.City, request.Description, request.ImageUrl);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new DeleteStudio(id), cancellationToken);
            return Ok(command);
        }

    }
}
