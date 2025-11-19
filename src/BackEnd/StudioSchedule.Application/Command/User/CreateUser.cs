using MediatR;
using StudioSchedule.Application.Services.Cryptography;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.UseCases.User;

public class CreateUser : IRequest<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUser, Guid>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUser request, CancellationToken cancellationToken)
    {

        var criptografiaDeSenha = new PasswordEncripter();
        
        var user = new Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Role = request.Role
        };
        
        user.PasswordHash = criptografiaDeSenha.Encrypt(request.PasswordHash);
        
        await _repository.CreateAsync(user);
        await _unitOfWork.Commit(cancellationToken);
        return user.Id;
    }
}