namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutDefinition
{
    public static LayoutDefinition Empty => new();

    //

    public string Name { get; } = null!;
    
    public int Version { get; } = 0;

    public int ColCount { get; private set; } = 0;

    public int RowCount { get; private set; } = 0;

    public IReadOnlyCollection<LayoutSectionDefinition> Sections { get; private set; } = [];

    //

    private LayoutDefinition() { }

    public LayoutDefinition(
        string name, 
        int version, 
        int colCount, 
        int rowCount, 
        IReadOnlyCollection<LayoutSectionDefinition> sections)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim().ToLowerInvariant();
        Version = version;
        ColCount = colCount;
        RowCount = rowCount;
        Sections = sections;
    }

    public LayoutDefinition(
        LayoutKey key,
        int colCount,
        int rowCount,
        IReadOnlyCollection<LayoutSectionDefinition> sections)
    {
        ArgumentNullException.ThrowIfNull(key);

        Name = key.Name;
        Version = key.Version;
        ColCount = colCount;
        RowCount = rowCount;
        Sections = sections;
    }

    public LayoutDefinition(
        string name,
        int version,
        GridDimensions dimensions,
        IReadOnlyCollection<LayoutSectionDefinition> sections)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim().ToLowerInvariant();
        Version = version;
        ColCount = dimensions.ColCount;
        RowCount = dimensions.RowCount;
        Sections = sections;
    }

    public LayoutDefinition(
        LayoutKey key,
        GridDimensions dimensions,
        IReadOnlyCollection<LayoutSectionDefinition> sections)
    {
        ArgumentNullException.ThrowIfNull(key);

        Name = key.Name;
        Version = key.Version;
        ColCount = dimensions.ColCount;
        RowCount = dimensions.RowCount;
        Sections = sections;
    }

    public LayoutDefinition(
        LayoutKey key,
        LayoutProps props)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(props);

        Name = key.Name;
        Version = key.Version;
        ColCount = props.ColCount;
        RowCount = props.RowCount;
        Sections = props.Sections;
    }

    //

    public LayoutKey Key => new(Name, Version);

    public LayoutProps Props => new(ColCount, RowCount, Sections);

    public GridDimensions Dimensions => new(ColCount, RowCount);
}