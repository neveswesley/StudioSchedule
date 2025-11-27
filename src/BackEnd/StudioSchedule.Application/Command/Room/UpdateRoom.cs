using FluentValidation.Results;
using MediatR;
using StudioSchedule.Application.Validators.Room;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Command.Room;

public class UpdateRoom: IRequest<UpdateRoomCommand>
{
    public string Name { get; set; } = string.Empty;
    public decimal HourPrice { get; set; }
    public int OpenHour { get; set; }
    public int CloseHour { get; set; }
    public string Description { get; set; } = string.Empty;
}

public sealed record UpdateRoomCommand(
    Guid Id,
    string Name,
    decimal HourPrice,
    int OpenHour,
    int CloseHour,
    string Description) : IRequest<Guid>;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Guid>
{
    
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);
        
        var entity = await _roomRepository.GetByIdAsync(request.Id);
        entity.Name = request.Name;
        entity.HourPrice = request.HourPrice;
        entity.OpenHour = request.OpenHour;
        entity.CloseHour = request.CloseHour;
        entity.Description = request.Description;
        
        _roomRepository.UpdateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        return entity.StudioId;
    }
    
    private async Task Validate(UpdateRoomCommand request)
    {
        var validator = new UpdateRoomValidator();
        var result = validator.Validate(request);

        var roomValidator = await _roomRepository.GetByIdAsync(request.Id);
        
        if (roomValidator == null)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.ROOM_NULL));
        }
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}