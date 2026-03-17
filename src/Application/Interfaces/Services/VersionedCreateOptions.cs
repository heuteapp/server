using HeuteApp.Application.Enums.Services;

namespace HeuteApp.Application.Interfaces.Services;

public class VersionedCreateOptions : CreateOptions
{
    public VersionedCreateBehavior VersionedBehavior { get; init; } = VersionedCreateBehavior.CreateNew;
}