using FluentValidation;
using StudioSchedule.Application.Command.Studio;
using StudioSchedule.Exceptions;

namespace StudioSchedule.Application.Validators.Studio;

public class UpdateStudioValidator : AbstractValidator<UpdateStudioCommand>
{
    public UpdateStudioValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.REQUIRED)
            .MinimumLength(3).WithMessage(ResourceMessagesException.AT_LEAST_THREE);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(ResourceMessagesException.REQUIRED);
        
        RuleFor(x=>x.City)
            .NotEmpty().WithMessage(ResourceMessagesException.REQUIRED);
        
        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage(ResourceMessagesException.REQUIRED)
            .MinimumLength(3).WithMessage(ResourceMessagesException.AT_LEAST_THREE);

        RuleFor(x => x.ImageUrl).NotEmpty().WithMessage(ResourceMessagesException.REQUIRED);
    }
}