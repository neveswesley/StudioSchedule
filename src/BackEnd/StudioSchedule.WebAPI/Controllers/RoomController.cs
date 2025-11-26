using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Room;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRoom entity, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(entity, cancellationToken);
            return Ok(command);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] Guid id, UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoomCommand(id, request.Name, request.HourPrice, request.OpenHour, request.CloseHour, request.Description);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoom(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}