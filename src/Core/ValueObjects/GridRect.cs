namespace HeuteApp.Core.ValueObjects;

public sealed record GridRect
{
    public int ColIndex { get; }

    public int RowIndex { get; }

    public int ColSpan { get; }

    public int RowSpan { get; }

    public GridRect(int colIndex, int rowIndex, int colSpan = 1, int rowSpan = 1)
    {
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    //

    public bool Overlaps(GridRect other)
    {
        return
            ColIndex < other.ColIndex + other.ColSpan &&
            ColIndex + ColSpan > other.ColIndex &&
            RowIndex < other.RowIndex + other.RowSpan &&
            RowIndex + RowSpan > other.RowIndex;
    }

    public bool Contains(GridRect other)
    {
        return
            other.ColIndex >= ColIndex &&
            other.RowIndex >= RowIndex &&
            other.ColIndex + other.ColSpan <= ColIndex + ColSpan &&
            other.RowIndex + other.RowSpan <= RowIndex + RowSpan;
    }
}