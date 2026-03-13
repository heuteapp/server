using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardPlacement
{        
    public static BoardCardPlacement Empty => new();

    //

    public string SectionName { get; private set; } = string.Empty;

    public int ColIndex { get; private set; } = -1;

    public int RowIndex { get; private set; } = -1;

    public int ColSpan { get; private set; } = 0;

    public int RowSpan { get; private set; } = 0;

    //

    public BoardCardPlacement() { }

    public BoardCardPlacement(string sectionName, int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        SectionName = sectionName;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    public BoardCardPlacement(LayoutSectionKey section, GridRect position)
    {
        SectionName = section.Name;
        ColIndex = position.ColIndex;
        RowIndex = position.RowIndex;
        ColSpan = position.ColSpan;
        RowSpan = position.RowSpan;
    }

    //

    public LayoutSectionKey Section => new(SectionName);

    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);

}