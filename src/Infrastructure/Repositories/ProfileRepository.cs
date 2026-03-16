using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;
using HeuteApp.Application.Interfaces.Repositories;
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

    public async Task<HeuteProfile?> GetByUsernameAsync(string username)
    {
        var user = await context.Profiles
            .FirstOrDefaultAsync(c => c.Username == username);

        return user;
    }

    public async Task<HeuteProfile?> GetByEmailAsync(string email)
    {
        var user = await context.Profiles
            .FirstOrDefaultAsync(c => c.Email == email);

        return user;
    }

    public async Task<HeuteProfile> CreateAsync(ProfileDefinition definition)
    {
        var user = HeuteProfileModel.Create(definition);

        context.Profiles.Add(user);
        return user;
    }
}