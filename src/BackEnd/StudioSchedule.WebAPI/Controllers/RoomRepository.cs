using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Room;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomRepository : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomRepository(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRoom entity, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(entity, cancellationToken);
            return Ok(command);
        }
    }
}