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
        
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(id, request.Name, request.Email);
            
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
        
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
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
