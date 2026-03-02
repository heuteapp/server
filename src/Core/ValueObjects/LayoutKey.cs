namespace HeuteApp.Core.ValueObjects;

public sealed record LayoutKey(
    Guid? OwnerId,
    string Name,
    int Version
);