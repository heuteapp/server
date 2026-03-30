using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionDefinition
{
    public static LayoutSectionDefinition Empty => new();

    //

    public string Name { get; } = null!;

    public int ColIndex { get; private set; } = -1;

    public int RowIndex { get; private set; } = -1;

    public int ColSpan { get; private set; } = 0;

    public int RowSpan { get; private set; } = 0;

    //

    private LayoutSectionDefinition() { }

    public LayoutSectionDefinition(string name, int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        Name = name;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    public LayoutSectionDefinition(LayoutSectionKey key, LayoutSectionProps props)
    {
        Name = key.Name;
        ColIndex = props.ColIndex;
        RowIndex = props.RowIndex;
        ColSpan = props.ColSpan;
        RowSpan = props.RowSpan;
    }

    //
    
    [JsonIgnore]
    public LayoutSectionKey Key => new(Name);

    [JsonIgnore]
    public LayoutSectionProps Props => new(Position);
    
    [JsonIgnore]
    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);
}