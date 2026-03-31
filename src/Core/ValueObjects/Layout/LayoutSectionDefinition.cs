using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionDefinition(
    string Name,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan)
{
    public static LayoutSectionDefinition Empty => new();

    //

    public LayoutSectionDefinition()
        : this(string.Empty, -1, -1, 0, 0) { }

    public LayoutSectionDefinition(string name, LayoutSectionProps props)
        : this(name, props.ColIndex, props.RowIndex, props.ColSpan, props.RowSpan) { }

    public LayoutSectionDefinition(LayoutSectionKey key, int colIndex, int rowIndex, int colSpan, int rowSpan)
        : this(key.Name, colIndex, rowIndex, colSpan, rowSpan) { }

    public LayoutSectionDefinition(LayoutSectionKey key, LayoutSectionProps props)
        : this(key.Name, props.ColIndex, props.RowIndex, props.ColSpan, props.RowSpan) { }

    //

    [JsonIgnore]
    public LayoutSectionKey Key => new(Name);

    [JsonIgnore]
    public LayoutSectionProps Props => new(ColIndex, RowIndex, ColSpan, RowSpan);

    [JsonIgnore]
    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);
}