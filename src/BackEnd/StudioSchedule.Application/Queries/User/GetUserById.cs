using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.User;

public class GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            var user = new UserResponse()
            {
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role
            };
            return user;
        }
    }
}