using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.User;

public class GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<List<UserResponse>>;

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetAllUsersWithStudios();

            var user = entity.Select(x => new UserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Role = x.Role,
                Studios = x.Studios.Select(x => new StudioResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                }).ToList() ?? new List<StudioResponse>()
            }).ToList();
            
            return user;
        }
    }
}
