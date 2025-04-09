using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> UsernameExistsAsync(string username, CancellationToken token = default);
}