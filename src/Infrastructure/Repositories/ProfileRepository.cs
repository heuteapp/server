using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Repositories;

public class ProfileRepository(HeuteDbContext context) : IProfileRepository
{
    public async Task<HeuteProfile?> GetByIdAsync(Guid userId)
    {
        var user = await context.Profiles
            .FirstOrDefaultAsync(c => c.Id == userId);

        return user;
    }

    public async Task<HeuteProfile?> GetByKeyAsync(ProfileKey key)
    {
        var user = await context.Profiles
            .FirstOrDefaultAsync(c => c.Name == key.Name);

        return user;
    }

    public async Task<HeuteProfile> CreateAsync(ProfileKey key, ProfileProps props)
    {
        var user = HeuteProfileModel.Create(new (key, props));

        context.Profiles.Add(user);
        return user;
    }
}