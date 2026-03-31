using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps(
    int ColCount, 
    int RowCount, 
    IReadOnlyCollection<LayoutSectionDefinition> Sections)
{
    public static LayoutProps Empty => new();

    //

    public LayoutProps() 
        : this(0, 0, []) { }
    
    public LayoutProps(GridDimensions dimensions, IReadOnlyCollection<LayoutSectionDefinition> sections)
        : this(dimensions.ColCount, dimensions.RowCount, sections) { }

    //

    [JsonIgnore]
    public GridDimensions Dimensions => new(ColCount, RowCount);
}