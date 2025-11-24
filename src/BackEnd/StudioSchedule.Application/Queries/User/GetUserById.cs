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
            var entity = await _repository.GetUserWithStudios(request.Id);
            if (entity == null)
                throw new NullReferenceException("User not found");
            
            var user = new UserResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role,
                Studios = entity.Studios.Select(s => new StudioResponse
                {
                    Id = s.Id,
                    OwnerId = s.UserId,
                    Name = s.Name,
                    Address = s.Address,
                    City = s.City,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    CreatedAt = s.CreatedAt,
                }).ToList() ?? new List<StudioResponse>()};
            
            return user;
        }
    }
}