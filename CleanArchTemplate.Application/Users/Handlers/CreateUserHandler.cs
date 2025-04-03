using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;
using CleanArchTemplate.Application.Users.Validators;
using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Application.Common;

namespace CleanArchTemplate.Application.Users.Handlers;

internal sealed class CreateUserHandler(IUserService userService, CreateUserRequestValidator validator)
    : RequestWithValidationHandler<CreateUserRequest, CreateUserRequestValidator, UserResponse>(validator),
        IHandler<CreateUserRequest, UserResponse>
{
    protected override Task<Result<UserResponse>> OnSuccessfullValidation(CreateUserRequest request)
        => userService.AddUser(request);
}