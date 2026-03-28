using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Application.Enums.Results.Profile.Repository;

namespace HeuteApp.Application.Results.Profile.Repository;

public record ProfileGetResult
{
    public HeuteProfile? Profile { get; init; }

    public ProfileGetStatus Status { get; init; }
    
    public bool IsSuccess => Status == ProfileGetStatus.Success;
}