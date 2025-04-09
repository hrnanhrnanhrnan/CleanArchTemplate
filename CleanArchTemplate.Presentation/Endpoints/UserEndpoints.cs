using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Application.Users.Requests;

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

    public static async Task<IResult> GetAllUsers(IUserService userService, CancellationToken token) 
    {
        var (users, error, isSuccess) = await userService.GetAllUserAsync(token);

        return isSuccess
            ? Results.Ok(users)
            : Results.NotFound(error);
    }

    public static async Task<IResult> GetUserById(int id, IUserService userService, CancellationToken token) 
    {
        var (users, error, isSuccess) = await userService.GetUserByIdAsync(id, token);

        return isSuccess
            ? Results.Ok(users)
            : Results.NotFound(error);
    }

    public static async Task<IResult> AddUser(CreateUserRequest request, IUserService userService, CancellationToken token) 
    {
        var (user, error, isSuccess) = await userService.AddUserAsync(request, token);

        return isSuccess
            ? Results.Ok(user)
            : Results.BadRequest(error);
    }

    public static async Task<IResult> UpdateUser(UpdateUserRequest request, IUserService userService, CancellationToken token) 
    {
        var (user, error, isSuccess) = await userService.UpdateUserAsync(request, token);

        return isSuccess
            ? Results.Ok(user)
            : Results.BadRequest(error);
    }

    public static async Task<IResult> RemoveUser(int id, IUserService userService, CancellationToken token) 
    {
        var (error, isSuccess) = await userService.RemoveUserAsync(id, token);

        return isSuccess
            ? Results.Ok()
            : Results.BadRequest(error);
    }
}