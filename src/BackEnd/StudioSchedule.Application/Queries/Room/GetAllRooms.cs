using MediatR;
using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Room;

public sealed record GetAllRooms : IRequest<PagedResult<RoomResponse>>
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinOpen { get; set; }
    public int? MaxClose { get; set; }
    
    // Paging
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetAllRoomsHandler : IRequestHandler<GetAllRooms, PagedResult<RoomResponse>>
{
    
    private readonly IRoomRepository _roomRepository;

    public GetAllRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<PagedResult<RoomResponse>> Handle(GetAllRooms request, CancellationToken cancellationToken)
    {
        var query = _roomRepository.Query();
        
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(r => r.Name.Contains(request.Name));
        
        if (request.MinPrice.HasValue)
            query = query.Where(s=> s.HourPrice >= request.MinPrice.Value);
        
        if (request.MaxPrice.HasValue)
            query = query.Where(s=> s.HourPrice <= request.MaxPrice.Value);
        
        if (request.MinOpen.HasValue)
            query = query.Where(x=>x.OpenHour >= request.MinOpen.Value);
        
        if (request.MaxClose.HasValue)
            query = query.Where(x=>x.OpenHour <= request.MaxClose.Value);

        var totalItems = await query.CountAsync(cancellationToken);
        
        var skip = (request.Page - 1) * request.PageSize;

        var items = await query
            .Skip(skip)
            .Take(request.PageSize)
            .Select(s => new RoomResponse
            {
                Name = s.Name,
                HourPrice = s.HourPrice,
                OpenHour = s.OpenHour,
                CloseHour = s.CloseHour,
                Description = s.Description,
            }).ToListAsync(cancellationToken);

        return new PagedResult<RoomResponse>()
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            Items = items
        };

    }
}