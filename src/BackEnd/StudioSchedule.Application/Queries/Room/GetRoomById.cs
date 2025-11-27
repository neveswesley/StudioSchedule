using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Room;

public sealed record GetRoomById(Guid Id) : IRequest<RoomResponse>;

public class GetRoomByIdHandle : IRequestHandler<GetRoomById, RoomResponse>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdHandle(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse> Handle(GetRoomById request, CancellationToken cancellationToken)
    {
        var entity = await _roomRepository.GetRoomByIdWithStudios(request.Id);
        
        return new RoomResponse()
        {
            Name = entity.Name,
            HourPrice = entity.HourPrice,
            OpenHour = entity.OpenHour,
            CloseHour = entity.CloseHour,
            Description = entity.Description
        };
        
    }
}