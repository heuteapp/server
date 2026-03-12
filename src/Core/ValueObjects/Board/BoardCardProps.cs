namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardProps
{
    public static BoardCardProps Empty => new();

    //

    public string Title { get; private set; } = null!;

    public string SectionName { get; private set; } = null!;

    public int ColIndex { get; private set; } = -1;

    public int RowIndex { get; private set; } = -1;

    public int ColSpan { get; private set; } = 0;

    public int RowSpan { get; private set; } = 0;

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

    public BoardCardProps(BoardCardContent content, BoardCardPlacement placement)
    {
        Title = content.Title;
        SectionName = placement.SectionName;
        ColIndex = placement.ColIndex;
        RowIndex = placement.RowIndex;
        ColSpan = placement.ColSpan;
        RowSpan = placement.RowSpan;
    }
    
    //

    public BoardCardContent Content => new(Title);

    public BoardCardPlacement Placement => new(SectionName, ColIndex, RowIndex, ColSpan, RowSpan);
}