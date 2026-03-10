namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition(
    ProfileKey Key,
    ProfileProps Props
);