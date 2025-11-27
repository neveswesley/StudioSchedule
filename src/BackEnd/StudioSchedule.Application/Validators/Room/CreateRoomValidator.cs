using System.Data;
using FluentValidation;
using StudioSchedule.Application.Command.Room;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Validators.Room;

public class CreateRoomValidator : AbstractValidator<CreateRoom>
{
    public CreateRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
            .Length(1, 100).WithMessage(ResourceMessagesException.NAME_LENGTH);

        RuleFor(x => x.HourPrice)
            .NotEmpty().WithMessage(ResourceMessagesException.HOUR_PRICE_EMPTY);

        RuleFor(x => x.OpenHour)
            .NotEmpty().WithMessage(ResourceMessagesException.OPEN_HOUR_EMPTY)
            .GreaterThan(0).WithMessage(ResourceMessagesException.OPEN_HOUR_LENGTH)
            .LessThan(24).WithMessage(ResourceMessagesException.OPEN_HOUR_LENGTH);
        
        RuleFor(x=>x.CloseHour)
            .NotEmpty().WithMessage(ResourceMessagesException.CLOSE_HOUR_EMPTY)
            .GreaterThan(0).WithMessage(ResourceMessagesException.CLOSE_HOUR_LENGTH)
            .LessThan(24).WithMessage(ResourceMessagesException.OPEN_HOUR_LENGTH);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY)
            .Length(1, 1000).WithMessage(ResourceMessagesException.DESCRIPTION_LENGTH);
    }
}