namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutDefinition(
    Guid Id,
    LayoutKey Key,
    LayoutProps Props
);