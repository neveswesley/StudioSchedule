using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Studio;

public sealed record GetAllStudios : IRequest<List<StudioResponse>>
{
}

public class GetAllStudiosQueryHandler : IRequestHandler<GetAllStudios, List<StudioResponse>>
{
    private readonly IStudioRepository _repository;

    public GetAllStudiosQueryHandler(IStudioRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudioResponse>> Handle(GetAllStudios request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAllStudiosWithRoom();

        var result = entity.Select(s => new StudioResponse
        {
            Id = s.Id,
            OwnerId = s.UserId,
            Name = s.Name,
            Address = s.Address,
            City = s.City,
            Description = s.Description,
            ImageUrl = s.ImageUrl,
            Rooms = s.Rooms.Select(r=> new RoomResponse()
            {
                Name = r.Name,
                HourPrice = r.HourPrice,
                OpenHour = r.OpenHour,
                CloseHour = r.CloseHour,
                Description = r.Description,
            }).ToList(),
            CreatedAt = s.CreatedAt,
        }).ToList();
        
        return result;
    }
}