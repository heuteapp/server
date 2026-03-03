namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionDefinition(
    Guid Id,
    LayoutSectionKey Key,
    LayoutSectionProps Props
);