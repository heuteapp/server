namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutDefinition(
    Guid OwnerId, 
    LayoutKey Key,
    LayoutProps Props
);