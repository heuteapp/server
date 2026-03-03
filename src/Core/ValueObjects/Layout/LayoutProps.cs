namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps
{
    public IReadOnlyCollection<LayoutSectionDefinition> Sections { get; }

    public LayoutProps(IEnumerable<LayoutSectionDefinition> sections)
    {
        Sections = sections.ToList().AsReadOnly();
    }
}