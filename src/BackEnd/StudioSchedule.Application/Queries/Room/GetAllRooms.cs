using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Room;

public sealed record GetAllRooms : IRequest<List<RoomResponse>>;

public class GetAllRoomsHandler : IRequestHandler<GetAllRooms, List<RoomResponse>>
{
    private readonly IRoomRepository _roomRepository;

    public GetAllRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<RoomResponse>> Handle(GetAllRooms request, CancellationToken cancellationToken)
    {
        var entity = await _roomRepository.GetAllRoomsWithStudios();
        var result = entity.Select(x => new RoomResponse()
        {
            Name = x.Name,
            HourPrice = x.HourPrice,
            OpenHour = x.OpenHour,
            CloseHour = x.CloseHour,
            Description = x.Description
        }).ToList();
            
        return result;
    }
}