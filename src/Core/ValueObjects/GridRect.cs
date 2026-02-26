namespace HeuteApp.Core.ValueObjects;

public readonly record struct GridRect(
    int Row,
    int Col,
    int RowSpan,
    int ColSpan)
{
    public bool Overlaps(GridRect other)
    {
        return
            Col < other.Col + other.ColSpan &&
            Col + ColSpan > other.Col &&
            Row < other.Row + other.RowSpan &&
            Row + RowSpan > other.Row;
    }

    public bool Contains(GridRect other)
    {
        return
            other.Col >= Col &&
            other.Row >= Row &&
            other.Col + other.ColSpan <= Col + ColSpan &&
            other.Row + other.RowSpan <= Row + RowSpan;
    }
}