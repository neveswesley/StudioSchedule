using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Booking;
using StudioSchedule.Application.Queries.Room;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/rooms/{roomId:guid}/slots")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public SlotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSlots(Guid roomId, [FromQuery] DateTime date, [FromQuery] int duration = 60)
        {
            
            //dps validar no lugar certo
            if (duration <= 0 || duration > 24 * 60)
                return BadRequest("Invalid duration");
            
            var query = new GetRoomSlotsQuery(RoomId: roomId, Date: date, DurationMinutes: duration);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        
        
    }
}
