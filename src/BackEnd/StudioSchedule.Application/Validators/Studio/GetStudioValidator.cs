using System.Data;
using FluentValidation;
using StudioSchedule.Application.Queries.Studio;

namespace StudioSchedule.Application.Validators.Studio;

public class GetStudioValidator : AbstractValidator<GetAllStudios>
{
    public GetStudioValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
        
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
        
    }
}