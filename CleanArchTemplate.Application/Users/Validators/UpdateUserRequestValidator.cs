using FluentValidation;
using CleanArchTemplate.Application.Users.Requests;

namespace CleanArchTemplate.Application.Users.Validators;

internal sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username can't be empty or null");
    }
}