using CleanArchTemplate.Application.Users.Requests;
using CleanArchTemplate.Application.Users.Responses;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Users;

internal static class UserMapper
{
    internal static UserResponse ToResponse(this User entity)
        => new(entity.Username);

    internal static User ToEntity(this CreateUserRequest request)
        => new() { Username = request.Username };

    internal static User ToEntity(this UpdateUserRequest request)
        => new() { Username = request.Username };
}