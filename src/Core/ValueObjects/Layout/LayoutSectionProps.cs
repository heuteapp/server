using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionProps
{
    public static LayoutSectionProps Empty => new();

    //

    public int ColIndex { get; private set; } = -1;

    public int RowIndex { get; private set; } = -1;

    public int ColSpan { get; private set; } = 0;

    public int RowSpan { get; private set; } = 0;

    //

    public LayoutSectionProps() { }

    public LayoutSectionProps(int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    public LayoutSectionProps(GridRect position)
    {
        ColIndex = position.ColIndex;
        RowIndex = position.RowIndex;
        ColSpan = position.ColSpan;
        RowSpan = position.RowSpan;
    }

    //

    [JsonIgnore]
    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);
}