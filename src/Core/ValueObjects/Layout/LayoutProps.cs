namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps(
    GridDimensions Dimensions,
    IReadOnlyCollection<LayoutSectionDefinition> Sections
);