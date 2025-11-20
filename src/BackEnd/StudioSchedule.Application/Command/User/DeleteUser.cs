using MediatR;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.UseCases.User;

public sealed record DeleteUserCommand (Guid Id) : IRequest<string>
{
    
}

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, string>
{
    
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new NullReferenceException("User not found");
        
        _userRepository.DeleteAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
       
        return "User deleted successfully";
    }
}