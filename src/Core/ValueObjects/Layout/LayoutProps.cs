namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutProps
{
    public static LayoutProps Empty => new();

    //

    public int ColCount { get; private set; } = 0;

    public int RowCount { get; private set; } = 0;

    public IReadOnlyCollection<LayoutSectionDefinition> Sections { get; private set; } = [];

    //

    private LayoutProps() { }

    public LayoutProps(
        int ColCount, 
        int RowCount, 
        IReadOnlyCollection<LayoutSectionDefinition> Sections)
    {
        this.ColCount = ColCount;
        this.RowCount = RowCount;
        this.Sections = Sections;
    }

    public LayoutProps(
        GridDimensions dimensions,
        IReadOnlyCollection<LayoutSectionDefinition> sections)
    {
        ColCount = dimensions.ColCount;
        RowCount = dimensions.RowCount;
        Sections = sections;
    }

    //

    public GridDimensions Dimensions => new(ColCount, RowCount);
}