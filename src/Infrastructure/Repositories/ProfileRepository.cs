using HeuteApp.Core.ValueObjects.Profile;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Profile.Repository;
using HeuteApp.Application.Enums.Results.Profile.Repository;

namespace HeuteApp.Infrastructure.Repositories;

public class ProfileRepository(HeuteDbContext context) : IProfileRepository
{
    public async Task<ProfileGetResult> GetByIdAsync(Guid userId)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Id == userId);

        return profile == null
            ? new ProfileGetResult
            {
                Profile = null,
                Status = ProfileGetStatus.NotFound
            }
            : new ProfileGetResult
            {
                Profile = profile,
                Status = ProfileGetStatus.Success
            };
    }

    public async Task<ProfileGetResult> GetByUsernameAsync(string username)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Username == username);

        return profile == null
            ? new ProfileGetResult
            {
                Profile = null,
                Status = ProfileGetStatus.NotFound
            }
            : new ProfileGetResult
            {
                Profile = profile,
                Status = ProfileGetStatus.Success
            };
    }

    public async Task<ProfileGetResult> GetByEmailAsync(string email)
    {
        var profile = await context.Profiles
            .FirstOrDefaultAsync(c => c.Email == email);

        return profile == null
            ? new ProfileGetResult
            {
                Profile = null,
                Status = ProfileGetStatus.NotFound
            }
            : new ProfileGetResult
            {
                Profile = profile,
                Status = ProfileGetStatus.Success
            };
    }

    public async Task<ProfileCreateResult> CreateAsync(ProfileDefinition definition)
    {
        var usernameExists = await context.Profiles
            .AnyAsync(p => p.Username == definition.Username);
        
        if (usernameExists)
        {
            return new ProfileCreateResult
            {
                Profile = null,
                Status = ProfileCreateStatus.UsernameAlreadyExists,
                ExistingIdentifier = definition.Username
            };
        }
        
        var emailExists = await context.Profiles
            .AnyAsync(p => p.Email == definition.Email);
        
        if (emailExists)
        {
            return new ProfileCreateResult
            {
                Profile = null,
                Status = ProfileCreateStatus.EmailAlreadyExists,
                ExistingIdentifier = definition.Email
            };
        }
        
        var profile = HeuteProfileModel.Create(definition);
        await context.Profiles.AddAsync(profile);
        
        return new ProfileCreateResult
        {
            Profile = profile,
            Status = ProfileCreateStatus.Success,
            ExistingIdentifier = null
        };
    }
}