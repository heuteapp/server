using HeuteApp.Core.ValueObjects.Profile;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Infrastructure.Repositories;

public class ProfileRepository(HeuteDbContext context) : IProfileRepository
{
    public async Task<ReadResult<HeuteProfile>> ReadByIdAsync(Guid userId)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Id == userId);

        return profile == null
            ? ReadResult<HeuteProfile>.NotFound()
            : ReadResult<HeuteProfile>.Success(profile);
    }

    public async Task<ReadResult<HeuteProfile>> ReadByUsernameAsync(string username)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Username == username);

        return profile == null
            ? ReadResult<HeuteProfile>.NotFound()
            : ReadResult<HeuteProfile>.Success(profile);
    }

    public async Task<ReadResult<HeuteProfile>> ReadByEmailAsync(string email)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Email == email);

        return profile == null
            ? ReadResult<HeuteProfile>.NotFound()
            : ReadResult<HeuteProfile>.Success(profile);
    }

    public async Task<CreateResult<HeuteProfile>> CreateAsync(ProfileDefinition definition)
    {
        var usernameExists = await context.Profiles
            .AnyAsync(p => p.Username == definition.Username);
        
        if (usernameExists)
        {
            return CreateResult<HeuteProfile>.AlreadyExists("profile", definition.Username);
        }
        
        var emailExists = await context.Profiles
            .AnyAsync(p => p.Email == definition.Email);
        
        if (emailExists)
        {
            return CreateResult<HeuteProfile>.AlreadyExists("profile", definition.Email);
        }
        
        var profile = HeuteProfileModel.Create(definition);
        await context.Profiles.AddAsync(profile);
        
        return CreateResult<HeuteProfile>.Success(profile);
    }
}