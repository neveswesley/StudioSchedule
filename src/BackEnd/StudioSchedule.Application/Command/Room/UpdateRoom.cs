using MediatR;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Command.Room;

public class UpdateRoomRequest: IRequest<UpdateRoomCommand>
{
    public string Name { get; set; } = string.Empty;
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; } = string.Empty;
}

public sealed record UpdateRoomCommand(
    Guid StudioId,
    string Name,
    decimal HourPrice,
    int OpenHour,
    int CloseHour,
    string Description) : IRequest<Guid>;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Guid>
{
    
    private readonly IRoomRepository _roomRepository;

    public UpdateRoomCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Guid> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roomRepository.GetByIdAsync(request.StudioId);
        entity.Name = request.Name;
        entity.HourPrice = request.HourPrice;
        entity.OpenHour = request.OpenHour;
        entity.CloseHour = request.CloseHour;
        entity.Description = request.Description;
        
        _roomRepository.UpdateAsync(entity);
        
        return entity.StudioId;
    }
}