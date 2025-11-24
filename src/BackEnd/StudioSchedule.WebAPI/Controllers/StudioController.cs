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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new GetAllStudios(), cancellationToken);
            return Ok(command);
        }
    }
}
