using CleanArchTemplate.Application.Users;
using CleanArchTemplate.Domain.Entities;
using CleanArchTemplate.Infrastructure.Contexts;

namespace CleanArchTemplate.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext context)
    : BaseRepository<User>(context),
        IUserRepository
{
    public async Task<bool> UsernameExists(string username)
    {
        var entites = (await Query(x => x.Username == username))
            .ToArray();

        return entites.Length != 0;
    }
}
