using MediatR;
using StudioSchedule.Application.Validators.Studio;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Command.Studio;

public sealed record CreateStudio : IRequest<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public Guid UserId { get; set; }
}

public class CreateStudioCommandHandler : IRequestHandler<CreateStudio, Guid>
{
    private readonly IStudioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudioCommandHandler(IStudioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateStudio request, CancellationToken cancellationToken)
    {
        
        await Validate(request);
        
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

        return entity.Id;
    }
    
    private async Task Validate(CreateStudio request)
    {
        var validator = new CreateStudioValidator();
        
        var result = validator.Validate(request);
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}