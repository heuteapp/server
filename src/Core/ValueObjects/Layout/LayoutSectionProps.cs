using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionProps(
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan)
{
    public static LayoutSectionProps Empty => new();

    //

    public LayoutSectionProps() 
        : this(-1, -1, 0, 0) { }

    public LayoutSectionProps(GridRect position)
        : this(position.ColIndex, position.RowIndex, position.ColSpan, position.RowSpan) { }

    //
    
    [JsonIgnore]
    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);
}