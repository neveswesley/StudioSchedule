using MediatR;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Command.Studio;

public sealed record DeleteStudio (Guid Id) : IRequest<Unit>;

public class DeleteStudioCommandHandler : IRequestHandler<DeleteStudio, Unit>
{

    private readonly IStudioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudioCommandHandler(IStudioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteStudio request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        _repository.DeleteAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}