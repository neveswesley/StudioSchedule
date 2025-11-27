using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using MediatR;
using StudioSchedule.Application.Validators.Room;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Command.Room;

public sealed record CreateRoom : IRequest<Guid>
{
    public Guid StudioId { get; set; }
    public string Name { get; set; }
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; }
}

public class CreateRoomHandler : IRequestHandler<CreateRoom, Guid>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IStudioRepository _studioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomHandler(IRoomRepository roomRepository, IStudioRepository studioRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _studioRepository = studioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateRoom request, CancellationToken cancellationToken)
    {
        await Validate(request);

        var entity = new Domain.Entities.Room()
        {
            StudioId = request.StudioId,
            Name = request.Name,
            HourPrice = request.HourPrice,
            OpenHour = request.OpenHour,
            CloseHour = request.CloseHour,
            Description = request.Description
        };

        await _roomRepository.CreateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        return entity.Id;
    }
    
    private async Task Validate(CreateRoom request)
    {
        var validator = new CreateRoomValidator();
        
        var result = validator.Validate(request);

        var studioIdValidation = await _studioRepository.GetByIdAsync(request.StudioId);
        
        if (studioIdValidation == null)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.STUDIO_NULL));
        }
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}