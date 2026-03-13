namespace HeuteApp.Core.ValueObjects.Board;

public record BoardCardDefinition
{
    public static BoardCardDefinition Empty => new();

    //

    public string Name { get; private set; } = null!;

    public string? Title { get; private set; } = null!;

    public string? SectionName { get; private set; } = null;

    public int? ColIndex { get; private set; } = null;

    public int? RowIndex { get; private set; } = null;

    public int? ColSpan { get; private set; } = null;

    public int? RowSpan { get; private set; } = null;

    //

    public BoardCardDefinition() { }

    public BoardCardDefinition(string name, string? title, string sectionName, int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        Name = name;
        Title = title;
        SectionName = sectionName;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }
    
    public BoardCardDefinition(BoardCardKey key, BoardCardProps props)
    {
        Name = key.Name;
        Title = props.Title;
        SectionName = props.SectionName;
        ColIndex = props.ColIndex;
        RowIndex = props.RowIndex;
        ColSpan = props.ColSpan;
        RowSpan = props.RowSpan;
    }

    //

    public BoardCardKey Key => new(Name);

    public BoardCardProps Props => new(Content, Placement);

    public BoardCardContent Content => new(Title);

    public BoardCardPlacement? Placement => 
        SectionName is not null && ColIndex is not null && RowIndex is not null && ColSpan is not null && RowSpan is not null
        ? new BoardCardPlacement(SectionName, ColIndex.Value, RowIndex.Value, ColSpan.Value, RowSpan.Value)
        : null;
}