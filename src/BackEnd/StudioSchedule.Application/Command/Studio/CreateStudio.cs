using MediatR;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;

namespace StudioSchedule.Application.Command.Studio;

public sealed record CreateStudio : IRequest<StudioResponse>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public Guid UserId { get; set; }
}

public class CreateStudioCommandHandler : IRequestHandler<CreateStudio, StudioResponse>
{
    private readonly IStudioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudioCommandHandler(IStudioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StudioResponse> Handle(CreateStudio request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Studio
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            UserId = request.UserId,
        };

        await _repository.CreateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        return new StudioResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            OwnerId = entity.UserId,
            CreatedAt = entity.CreatedAt
        };
    }
}