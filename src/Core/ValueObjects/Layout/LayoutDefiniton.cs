using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutDefinition(
    string Name,
    int Version,
    int ColCount,
    int RowCount,
    IReadOnlyCollection<LayoutSectionDefinition> Sections)
{
    public static LayoutDefinition Empty => new();

    //

    public LayoutDefinition() 
        : this(string.Empty, 0, 0, 0, []) { }

    public LayoutDefinition(string name, int version, LayoutProps props)
        : this(name, version, props.ColCount, props.RowCount, props.Sections) { }

    public LayoutDefinition(LayoutKey key, int colCount, int rowCount, IReadOnlyCollection<LayoutSectionDefinition> sections)
        : this(key.Name, key.Version, colCount, rowCount, sections) { }

    public LayoutDefinition(LayoutKey key, LayoutProps props)
        : this(key.Name, key.Version, props.ColCount, props.RowCount, props.Sections) { }

    //

    [JsonIgnore]
    public LayoutKey Key => new(Name, Version);
    
    [JsonIgnore]
    public LayoutProps Props => new(ColCount, RowCount, Sections);

    [JsonIgnore]
    public GridDimensions Dimensions => new(ColCount, RowCount);
}