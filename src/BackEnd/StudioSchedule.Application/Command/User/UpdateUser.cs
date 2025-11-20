using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.UseCases.User;

public sealed record UpdateUserRequest() : IRequest<UpdateUserCommand>
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public sealed record UpdateUserCommand(
    Guid Id,
    string Name,
    string Email
) : IRequest<UserResponse>;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new NullReferenceException("User do not exist.");

        entity.Name = request.Name;
        entity.Email = request.Email;

        _repository.UpdateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        return new UserResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Role = entity.Role
        };
    }
}