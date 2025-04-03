using CleanArchTemplate.Application.Common;
using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;

namespace CleanArchTemplate.Application.Users;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetAllUser();
    Task<Result<UserResponse>> GetUserById(int id);
    Task<Result<UserResponse>> AddUser(CreateUserRequest request);
    Task<Result<UserResponse>> UpdateUser(UpdateUserRequest request);
    Task<Result> RemoveUser(int id);
}