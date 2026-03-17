using HeuteApp.Application.Enums.Services;

namespace HeuteApp.Application.Interfaces.Services;

public class VersionedCreateOptions : CreateOptions
{
    public VersionedCreateBehavior Behavior { get; init; } = VersionedCreateBehavior.CreateNew;
}