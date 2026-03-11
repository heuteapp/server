using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Interfaces;

public interface IProfileRepository
{    
    Task<HeuteProfile?> GetByIdAsync(Guid userId);

    Task<HeuteProfile?> GetByNameAsync(string name);

    Task<HeuteProfile> CreateAsync(ProfileDefinition definition);
}