namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionIdentity(
    Guid Id,
    LayoutSectionKey Key,
    LayoutSectionProps Props
);