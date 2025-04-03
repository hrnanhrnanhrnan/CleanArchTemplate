using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;
using CleanArchTemplate.Application.Common.Interfaces;

namespace CleanArchTemplate.Presentation.Endpoints;

internal static class UserEndpoints
{
    private const string RoutePrefix = "user";

    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix);

        group.MapGet("/", GetAllUsers);
        group.MapGet("/{id}", GetUserById);
        group.MapPost("/add", AddUser);
        group.MapPut("/update", UpdateUser);
        group.MapDelete("/remove/{id}", RemoveUser);

        return group;
    }

    public static async Task<IResult> GetAllUsers(IUserService userService) 
    {
        var (users, error, isSuccess) = await userService.GetAllUser();

        return isSuccess
            ? Results.Ok(users)
            : Results.NotFound(error);
    }

    public static async Task<IResult> GetUserById(int id, IUserService userService) 
    {
        var (users, error, isSuccess) = await userService.GetUserById(id);

        return isSuccess
            ? Results.Ok(users)
            : Results.NotFound(error);
    }

    public static async Task<IResult> AddUser(CreateUserRequest request, IHandler<CreateUserRequest, UserResponse> handler) 
    {
        var (user, error, isSuccess) = await handler.InvokeAsync(request);

        return isSuccess
            ? Results.Ok(user)
            : Results.Conflict(error);
    }

    public static async Task<IResult> UpdateUser(UpdateUserRequest request, IUserService userService) 
    {
        var (user, error, isSuccess) = await userService.UpdateUser(request);

        return isSuccess
            ? Results.Ok(user)
            : Results.BadRequest(error);
    }

    public static async Task<IResult> RemoveUser(int id, IUserService userService) 
    {
        var (error, isSuccess) = await userService.RemoveUser(id);

        return isSuccess
            ? Results.Ok()
            : Results.BadRequest(error);
    }
}