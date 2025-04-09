using CleanArchTemplate.Application.Common;
using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;

namespace CleanArchTemplate.Application.Users;

internal sealed class UserService(IUserRepository userRepository, IRequestValidator requestValidator) 
    : IUserService
{
    public async Task<Result<IEnumerable<UserResponse>>> GetAllUserAsync(CancellationToken token = default)
    {
        var users = (await userRepository.GetAllAsync(token))
            .ToArray();

        return users.Length == 0
            ? Result<IEnumerable<UserResponse>>.Failure("No users found")
            : Result<IEnumerable<UserResponse>>.Success(users.Select(x => x.ToResponse()));
    }

    public async Task<Result<UserResponse>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, token: cancellationToken);

        return user is null
            ? Result<UserResponse>.Failure($"Could not find user with id: {id}")
            : Result<UserResponse>.Success(user.ToResponse());
    }

    public async Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken token = default)
    {
        var validationResult = await requestValidator.ValidateAsync(request, token);
        
        if (!validationResult.IsValid)
        {
            return Result<UserResponse>.Failure(validationResult.ToString(", "));
        }

        var usernameExists = await userRepository.UsernameExistsAsync(request.Username, token);

        if (usernameExists)
        {
            return Result<UserResponse>.Failure($"Username {request.Username} already exists");
        }

        var addedUser = await userRepository.AddAsync(request.ToEntity(), token);

        return Result<UserResponse>.Success(addedUser.ToResponse());
    }

    public async Task<Result> RemoveUserAsync(int id, CancellationToken token = default)
    {
        var user = await userRepository.GetByIdAsync(id, token: token);

        if (user is null)
        {
            return Result.Failure($"Could not find user with id: {id}");
        }

        await userRepository.RemoveAsync(user, token);
        return Result.Success();
    }

    public async Task<Result<UserResponse>> UpdateUserAsync(UpdateUserRequest request, CancellationToken token = default)
    {
        var validationResult = await requestValidator.ValidateAsync(request, token);

        if (!validationResult.IsValid)
        {
            return Result<UserResponse>.Failure(validationResult.ToString(", "));
        }

        var user = await userRepository.GetByIdAsync(request.Id, true, token);

        if (user is null)
        {
            return Result<UserResponse>.Failure($"Could not find user with id: {request.Id}");
        }

        user.Username = request.Username;
        var updatedEntity = await userRepository.UpdateAsync(user, token);
        return Result<UserResponse>.Success(updatedEntity.ToResponse());
    }
}