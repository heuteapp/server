using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Application.Enums.Results.Profile.Repository;

namespace HeuteApp.Application.Results.Profile.Repository;

public record ProfileCreateResult
{
    public HeuteProfile? Profile { get; init; }
    
    public ProfileCreateStatus Status { get; init; }

    public string? ExistingIdentifier { get; init; }
    
    public bool IsSuccess => Status == ProfileCreateStatus.Success;
}