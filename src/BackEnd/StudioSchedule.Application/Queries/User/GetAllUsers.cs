using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.User;

public class GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<IEnumerable<UserResponse>>;
    
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
    {
        
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetAllAsync();
            var users = entity.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Role = u.Role
            });
            
            return users;
        }
    }
}