using MediatR;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Command.Room;

public sealed record DeleteRoom(Guid Id) : IRequest<Unit>;

public class DeleteRoomHandler : IRequestHandler<DeleteRoom, Unit>
{
    
    private readonly IRoomRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomHandler(IRoomRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteRoom request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        _repository.DeleteAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}