namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutDefinition(
    string Name,
    int Version,
    int ColCount,
    int RowCount,
    IReadOnlyCollection<LayoutSectionDefinition> Sections)
{
    public static LayoutDefinition Empty => new("", 0, 0, 0, []);

    //

    public LayoutDefinition(string name, int version, LayoutProps props)
        : this(name, version, props.ColCount, props.RowCount, props.Sections) { }

    public LayoutDefinition(LayoutKey key, int colCount, int rowCount, IReadOnlyCollection<LayoutSectionDefinition> sections)
        : this(key.Name, key.Version, colCount, rowCount, sections) { }

    public LayoutDefinition(LayoutKey key, LayoutProps props)
        : this(key.Name, key.Version, props.ColCount, props.RowCount, props.Sections) { }

    //

    public LayoutKey Key => new(Name, Version);

    public LayoutProps Props => new(ColCount, RowCount, Sections);

    public GridDimensions Dimensions => new(ColCount, RowCount);
}