namespace HeuteApp.Core.ValueObjects.User;

public sealed record UserDefinition(
    UserKey Key,
    UserProps Props
);