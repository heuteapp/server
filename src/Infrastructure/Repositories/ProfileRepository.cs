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

    public async Task<HeuteProfile?> GetByNameAsync(string name)
    {
        var user = await context.Profiles
            .FirstOrDefaultAsync(c => c.Name == name);

        return user;
    }

    public async Task<HeuteProfile> CreateAsync(ProfileDefinition definition)
    {
        var user = HeuteProfileModel.Create(definition);

        context.Profiles.Add(user);
        return user;
    }
}