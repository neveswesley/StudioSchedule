using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Command.Booking;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(CreateBooking request, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(request, cancellationToken);
            return Ok(command);
        }
    }
}
