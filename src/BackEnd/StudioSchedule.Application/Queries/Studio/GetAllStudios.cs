using MediatR;
using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Queries.Studio;

public sealed record GetAllStudios : IRequest<PagedResult<StudioResponse>>
{
    // Filters
    public string? City { get; set; }
    public string? Name { get; set; }
    
    // Paging
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetAllStudiosQueryHandler : IRequestHandler<GetAllStudios, PagedResult<StudioResponse>>
{
    private readonly IStudioRepository _repository;

    public GetAllStudiosQueryHandler(IStudioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<StudioResponse>> Handle(GetAllStudios request, CancellationToken cancellationToken)
    {
        var query = _repository.Query();

        // Filters
        if (!string.IsNullOrEmpty(request.City))
            query = query.Where(x => x.City.Contains(request.City));

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{request.Name}%"));

        var totalItems = await query.CountAsync(cancellationToken);

        query = query.OrderBy(s => s.Name);

        var skip = (request.Page - 1) * request.PageSize;
        
        var items = await query
            .Skip(skip)
            .Take(request.PageSize)
            .Include(s => s.Rooms)
            .Select(s => new StudioResponse
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                City = s.City,
                Description = s.Description,
                ImageUrl = s.ImageUrl,
                Rooms = s.Rooms.Select(r => new RoomResponse
                {
                    Name = r.Name,
                    HourPrice = r.HourPrice,
                    OpenHour = r.OpenHour,
                    CloseHour = r.CloseHour,
                    Description = r.Description,
                }).ToList()
            }).ToListAsync(cancellationToken);

        return new PagedResult<StudioResponse>()
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            Items = items,
        };
    }
    
}