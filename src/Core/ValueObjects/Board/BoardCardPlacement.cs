using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardPlacement
{
    public string SectionName { get; private set; } = null!;

    public int Col { get; private set; }

    public int Row { get; private set; }

    public int ColSpan { get; private set; }

    public int RowSpan { get; private set; }

    //

    public LayoutSectionKey Section => new(SectionName);

    public GridRect Position => new(Col, Row, ColSpan, RowSpan);

    //

    private BoardCardPlacement() { }

    public BoardCardPlacement(LayoutSectionKey section, GridRect position)
    {
        SectionName = section.Name;
        Col = position.Col;
        Row = position.Row;
        ColSpan = position.ColSpan;
        RowSpan = position.RowSpan;
    }
}