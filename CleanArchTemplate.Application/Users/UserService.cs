using CleanArchTemplate.Application.Common;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;

namespace CleanArchTemplate.Application.Users;

internal sealed class UserService(IUserRepository userRepository) 
    : IUserService
{
    public async Task<Result<IEnumerable<UserResponse>>> GetAllUser()
    {
        var users = (await userRepository.GetAllAsync())
            .ToArray();

        return users.Length == 0
            ? Result<IEnumerable<UserResponse>>.Failure("No users found")
            : Result<IEnumerable<UserResponse>>.Success(users.Select(x => x.ToResponse()));
    }

    public async Task<Result<UserResponse>> GetUserById(int id)
    {
        var user = await userRepository.GetById(id);

        return user is null
            ? Result<UserResponse>.Failure($"Could not find user with id: {id}")
            : Result<UserResponse>.Success(user.ToResponse());
    }

    public async Task<Result<UserResponse>> AddUser(CreateUserRequest request)
    {
        var usernameExists = await userRepository.UsernameExists(request.Username);

        if (usernameExists)
        {
            return Result<UserResponse>.Failure($"Username {request.Username} already exists");
        }

        var addedUser = await userRepository.AddAsync(request.ToEntity());

        return Result<UserResponse>.Success(addedUser.ToResponse());
    }

    public async Task<Result> RemoveUser(int id)
    {
        var user = await userRepository.GetById(id);

        if (user is null)
        {
            return Result.Failure($"Could not find user with id: {id}");
        }

        await userRepository.RemoveAsync(user);
        return Result.Success();
    }

    public async Task<Result<UserResponse>> UpdateUser(UpdateUserRequest request)
    {
        var user = await userRepository.GetById(request.Id, true);

        if (user is null)
        {
            return Result<UserResponse>.Failure($"Could not find user with id: {request.Id}");
        }

        user.Username = request.Username;
        var updatedEntity = await userRepository.UpdateAsync(user);
        return Result<UserResponse>.Success(updatedEntity.ToResponse());
    }
}