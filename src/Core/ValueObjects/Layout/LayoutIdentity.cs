namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutIdentity(
    Guid Id,
    LayoutKey Key,
    LayoutProps Props
);