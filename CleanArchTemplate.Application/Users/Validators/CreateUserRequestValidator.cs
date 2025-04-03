using FluentValidation;
using CleanArchTemplate.Application.Users.Requests;

namespace CleanArchTemplate.Application.Users.Validators;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username can't be empty or null");
        
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password can't be empty or null");
    }
}