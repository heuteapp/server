namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps(
    int ColCount, 
    int RowCount, 
    IReadOnlyCollection<LayoutSectionDefinition> Sections)
{
    public static LayoutProps Empty => new(0, 0, []);

    //
    
    public LayoutProps(GridDimensions dimensions, IReadOnlyCollection<LayoutSectionDefinition> sections)
        : this(dimensions.ColCount, dimensions.RowCount, sections) { }

    //

    public GridDimensions Dimensions => new(ColCount, RowCount);
}