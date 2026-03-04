using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.User;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Repositories;

public class UserRepository(HeuteDbContext context) : IUserRepository
{
    public async Task<HeuteUser?> GetByIdAsync(Guid userId)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(c => c.Id == userId);

        return user;
    }

    public async Task<HeuteUser?> GetByKeyAsync(UserKey key)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(c => c.Name == key.Name);

        return user;
    }

    public async Task<HeuteUser> CreateAsync(UserKey key, UserProps props)
    {
        var user = HeuteUserModel.Create(new (key, props));

        context.Users.Add(user);
        return user;
    }
}