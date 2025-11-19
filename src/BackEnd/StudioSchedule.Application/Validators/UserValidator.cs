using FluentValidation;
using StudioSchedule.Application.UseCases.User;
using StudioSchedule.Domain.DTO;
using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Application.Validators;

public class UserValidator : AbstractValidator<CreateUser>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress().WithMessage("Invalid email address");
        RuleFor(user => user.PasswordHash).NotEmpty().WithMessage("Password cannot be empty").MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(user => user.Role).NotEmpty().WithMessage("Role cannot be empty");
    }
}