using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Room;
using StudioSchedule.Application.Queries.Room;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, UpdateRoom request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoomCommand(id, request.Name, request.HourPrice, request.OpenHour, request.CloseHour, request.Description);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new DeleteRoom(id), cancellationToken);
            return Ok(command);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new GetAllRooms(), cancellationToken);
            return Ok(command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(new GetRoomById(id), cancellationToken);
            return Ok(command);
        }
    }
}