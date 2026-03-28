using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services.Public;

public class PublicProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteProfile> GetProfileByIdentifierAsync(string identifier)
    {
        var isEmail = identifier.Contains('@');
        
        var result = isEmail 
            ? await repository.GetByEmailAsync(identifier) 
            : await repository.GetByUsernameAsync(identifier);

        if (!result.IsSuccess || result.Profile == null)
        {
            throw new Exception($"Profile with {(isEmail ? "email" : "username")} '{identifier}' not found.");
        }

        return result.Profile;
    }

    public async Task<HeuteProfile> CreateProfileAsync(ProfileDefinition definition)
    {
        var result = await repository.CreateAsync(definition);

        if (!result.IsSuccess || result.Profile == null)
        {
            throw new Exception($"Failed to create profile: {result.Status}");
        }

        await unitOfWork.SaveChangesAsync();

        return result.Profile;
    }
}