using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Studio;

public sealed record GetStudioById (Guid Id) : IRequest<StudioResponse>;

public class Handler : IRequestHandler<GetStudioById, StudioResponse>
{
    
    private readonly IStudioRepository _repository;

    public Handler(IStudioRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudioResponse> Handle(GetStudioById request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetStudioWithRoom(request.Id);
        
        var result = new StudioResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            Rooms = entity.Rooms.Select(r=> new RoomResponse()
            {
                Name = r.Name,
                HourPrice = r.HourPrice,
                OpenHour = r.OpenHour,
                CloseHour = r.CloseHour,
                Description = r.Description,
            }).ToList(),
        };

        return result;
    }
}