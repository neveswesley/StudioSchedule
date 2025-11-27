using MediatR;
using StudioSchedule.Application.Validators.Studio;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Command.Studio;

public sealed record UpdateStudioRequest : IRequest<UpdateStudioCommand>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}

public sealed record UpdateStudioCommand(
    Guid Id,
    string Name,
    string Address,
    string City,
    string Description,
    string ImageUrl
) : IRequest<StudioResponse>;

public class UpdateStudioHandler : IRequestHandler<UpdateStudioCommand, StudioResponse>
{
    
    private readonly IStudioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudioHandler(IStudioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StudioResponse> Handle(UpdateStudioCommand request, CancellationToken cancellationToken)
    {
        
        await Validate(request);
        
        var entity = await _repository.GetByIdAsync(request.Id);
        
        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.City = request.City;
        entity.Description = request.Description;
        entity.ImageUrl = request.ImageUrl;
        
        _repository.UpdateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        var result = new StudioResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
        };
        
        return result;
    }
    
    private async Task Validate(UpdateStudioCommand request)
    {
        var validator = new UpdateStudioValidator();
        
        var result = validator.Validate(request);
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}