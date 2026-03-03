namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps(
    IReadOnlyCollection<LayoutSectionIdentity> Sections
);