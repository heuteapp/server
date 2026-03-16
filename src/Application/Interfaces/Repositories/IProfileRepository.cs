using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IProfileRepository
{    
    Task<HeuteProfile?> GetByIdAsync(Guid userId);

    Task<HeuteProfile?> GetByUsernameAsync(string username);

    Task<HeuteProfile?> GetByEmailAsync(string email);

    Task<HeuteProfile> CreateAsync(ProfileDefinition definition);
}