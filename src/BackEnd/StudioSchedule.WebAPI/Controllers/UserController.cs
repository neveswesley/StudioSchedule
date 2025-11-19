using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioSchedule.Application.Queries.User;
using StudioSchedule.Application.UseCases.User;

namespace StudioSchedule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Post(CreateUser request, CancellationToken cancellationToken)
        {
            var command = await _mediator.Send(request, cancellationToken);
            return Ok(command);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetUserById.GetUserByIdQuery(id), cancellationToken);
            return Ok(query);
        }
        
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetAllUsers.GetAllUsersQuery(), cancellationToken);
            return Ok(query);
        }
        
    }
}
