namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps(
    GridSize Size,
    IReadOnlyCollection<LayoutSectionDefinition> Sections
);