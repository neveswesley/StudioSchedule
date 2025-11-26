using MediatR;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Command.Room;

public sealed record CreateRoom : IRequest<Guid>
{
    public Guid StudioId { get; set; }
    public string Name { get; set; }
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; }
}

public class CreateRoomHandler : IRequestHandler<CreateRoom, Guid>
{
    private readonly IRoomRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomHandler(IRoomRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateRoom request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Room()
        {
            StudioId = request.StudioId,
            Name = request.Name,
            HourPrice = request.HourPrice,
            OpenHour = request.OpenHour,
            CloseHour = request.CloseHour,
            Description = request.Description
        };

        await _repository.CreateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        return entity.Id;
    }
}