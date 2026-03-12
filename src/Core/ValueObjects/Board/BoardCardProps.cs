namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardProps
{
    public static BoardCardProps Empty => new();

    //

    public string Title { get; private set; } = null!;

    public string? SectionName { get; private set; } = null;

    public int? ColIndex { get; private set; } = null;

    public int? RowIndex { get; private set; } = null;

    public int? ColSpan { get; private set; } = null;

    public int? RowSpan { get; private set; } = null;

    //

    private BoardCardProps() { }

    public BoardCardProps(string title, string sectionName, int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        Title = title;
        SectionName = sectionName;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    public BoardCardProps(BoardCardContent content, BoardCardPlacement? placement)
    {
        Title = content.Title;
        SectionName = placement?.SectionName;
        ColIndex = placement?.ColIndex;
        RowIndex = placement?.RowIndex;
        ColSpan = placement?.ColSpan;
        RowSpan = placement?.RowSpan;
    }
    
    //

    public BoardCardContent Content => new(Title);

    public BoardCardPlacement? Placement => 
        SectionName is not null && ColIndex is not null && RowIndex is not null && ColSpan is not null && RowSpan is not null
        ? new BoardCardPlacement(SectionName, ColIndex.Value, RowIndex.Value, ColSpan.Value, RowSpan.Value)
        : null;
}