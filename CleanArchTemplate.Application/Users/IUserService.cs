using CleanArchTemplate.Application.Common;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;

namespace CleanArchTemplate.Application.Users;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetAllUserAsync(CancellationToken token = default);
    Task<Result<UserResponse>> GetUserByIdAsync(int id, CancellationToken token = default);
    Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken token = default);
    Task<Result<UserResponse>> UpdateUserAsync(UpdateUserRequest request, CancellationToken token = default);
    Task<Result> RemoveUserAsync(int id, CancellationToken token = default);
}