using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services.Public;

public class PublicProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<ProfileResult> GetProfileByIdentifierAsync(string identifier)
    {
        var isEmail = identifier.Contains('@');
        
        var result = isEmail 
            ? await repository.ReadByEmailAsync(identifier) 
            : await repository.ReadByUsernameAsync(identifier);

        result.ThrowIfFailure($"Failed to retrieve profile with identifier {identifier}");

        return result.Entity!.ToResult();
    }

    public async Task<ProfileResult> CreateProfileAsync(ProfileDefinition definition)
    {
        var result = await repository.CreateAsync(definition);
        result.ThrowIfFailure("Failed to create profile");

        await unitOfWork.SaveChangesAsync();

        return result.Entity!.ToResult();
    }

    public async Task<ProfileResult> GetProfileByEmailAsync(string email)
    {
        var result = await repository.ReadByEmailAsync(email);
        result.ThrowIfFailure($"Failed to retrieve profile with email: {email}");
        
        return result.Entity!.ToResult();
    }
    
    public async Task<ProfileResult> GetProfileByUsernameAsync(string username)
    {
        var result = await repository.ReadByUsernameAsync(username);
        result.ThrowIfFailure($"Failed to retrieve profile with username: {username}");
        
        return result.Entity!.ToResult();
    }
}