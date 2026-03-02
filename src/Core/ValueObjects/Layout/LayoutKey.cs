namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutKey(
    Guid? OwnerId,
    string Name,
    int Version
);