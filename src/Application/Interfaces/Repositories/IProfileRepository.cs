using HeuteApp.Application.Results.Profile.Repository;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IProfileRepository
{    
    Task<ProfileGetResult> GetByIdAsync(Guid userId);

    Task<ProfileGetResult> GetByUsernameAsync(string username);

    Task<ProfileGetResult> GetByEmailAsync(string email);

    Task<ProfileCreateResult> CreateAsync(ProfileDefinition definition);
}