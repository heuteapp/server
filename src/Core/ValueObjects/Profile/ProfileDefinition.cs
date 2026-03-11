namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition(
    Guid Id,
    ProfileProps Props
);