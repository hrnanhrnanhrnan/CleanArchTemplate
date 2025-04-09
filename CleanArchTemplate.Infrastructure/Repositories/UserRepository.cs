using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Domain.Entities;
using CleanArchTemplate.Infrastructure.Contexts;

namespace CleanArchTemplate.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext context)
    : BaseRepository<User>(context),
        IUserRepository
{
    public async Task<bool> UsernameExistsAsync(string username, CancellationToken token = default)
    {
        var entites = (await Query(x => x.Username == username, token: token))
            .ToArray();

        return entites.Length != 0;
    }
}
